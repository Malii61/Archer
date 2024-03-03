using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; } // Singleton instance of the EnemySpawner.
    public event EventHandler<Vector2> OnEnemyDied; // Event triggered when an enemy dies.
    [SerializeField] private AllEnemiesSO _allEnemiesSo; // Scriptable Object containing all enemy types.
    [SerializeField] private float spawnRate; 
    [SerializeField] private int maxEnemyCount; 
    private Transform _currentEnemy;
    private float _spawnerTimer; 
    private int _currentEnemyCount; 
    private Transform _playerTransform;
    private bool _isSpawnable = false; 
    public int DiedEnemyCountTotal; 

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateEnemyPool();
        _playerTransform = Player.Instance.transform; 
        GameManager.Instance.OnStageChanged += OnStageChanged;
    }

    private void OnStageChanged(object sender, GameManager.GameState e)
    {
        // Update the spawnability flag based on the game state.
        _isSpawnable = e switch
        {
            GameManager.GameState.GameOver => false,
            GameManager.GameState.Started => true,
            _ => _isSpawnable
        };
    }

    private void CreateEnemyPool()
    {
        // Create object pool for each enemy type.
        foreach (var enemySO in _allEnemiesSo.Enemies)
        {
            PoolHandler.Instance.Create(enemySO.enemyPrefab, PoolHandler.Instance.GetEnemyPoolType(enemySO.enemyType),
                10, maxEnemyCount);
        }
    }

    private EnemySO GetRandomEnemy()
    {
        return _allEnemiesSo.Enemies[Random.Range(0, _allEnemiesSo.Enemies.Count)]; // Get a random enemy type.
    }

    private void LateUpdate()
    {
        if (!_isSpawnable) return; // Do not spawn enemies if not in a spawnable state.

        _spawnerTimer -= Time.deltaTime; 
        if (_spawnerTimer <= 0f && _currentEnemyCount < maxEnemyCount)
        {
            EnemySO enemySO = GetRandomEnemy(); // Get a random enemy type.
            _currentEnemy = PoolHandler.Instance.Get(PoolHandler.Instance.GetEnemyPoolType(enemySO.enemyType));

            SetEnemyPosition(); // Set the position of the spawned enemy.
            while (Vector2.Distance(_currentEnemy.position, _playerTransform.position) < 1.5f)
            {
                SetEnemyPosition(); // Ensure the enemy is not spawned too close to the player.
            }

            // Initialize the enemy with specific attributes.
            _currentEnemy.GetComponent<EnemyController>()
                .Initialize(new EnemyInitializationArgs(enemySO.enemyRange, enemySO.enemyDamage,
                    enemySO.enemyMovementSpeed, enemySO.health, enemySO.attackSpeed), enemySO.enemyType);

            _spawnerTimer = spawnRate; 
            _currentEnemyCount++; // Increase the count of spawned enemies.
        }
    }

    private void SetEnemyPosition()
    {
        // Set the position of the enemy to a random position around the player.
        _currentEnemy.position = Utils.GetRandomPositionAtCertainPoint(_playerTransform.position, 4f);
    }

    public void EnemyDied(Vector2 diePosition)
    {
        OnEnemyDied?.Invoke(this, diePosition); 
        _currentEnemyCount--; // Decrease the count of spawned enemies.
        DiedEnemyCountTotal++; // Increase the total count of died enemies.
    }
}
