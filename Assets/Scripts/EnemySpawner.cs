using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    public event EventHandler<Vector2> OnEnemyDied;
    [SerializeField] private AllEnemiesSO _allEnemiesSo;
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxEnemyCount;
    private Transform _currentEnemy;
    private float _spawnerTimer;
    private int _currentEnemyCount;
    private Transform _playerTransform;
    private bool _isSpawnable = true;

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
        if (e == GameManager.GameState.GameOver)
        {
            _isSpawnable = false;
        }
    }

    private void CreateEnemyPool()
    {
        foreach (var enemySO in _allEnemiesSo.Enemies)
        {
            PoolHandler.Instance.Create(enemySO.enemyPrefab, PoolHandler.Instance.GetEnemyPoolType(enemySO.enemyType),
                10, maxEnemyCount);
        }
    }

    private EnemySO GetRandomEnemy()
    {
        return _allEnemiesSo.Enemies[Random.Range(0, _allEnemiesSo.Enemies.Count)];
    }

    private void LateUpdate()
    {
        if (!_isSpawnable) return;

        _spawnerTimer -= Time.deltaTime;
        if (_spawnerTimer <= 0f && _currentEnemyCount < maxEnemyCount)
        {
            EnemySO enemySO = GetRandomEnemy();
            _currentEnemy = PoolHandler.Instance.Get(PoolHandler.Instance.GetEnemyPoolType(enemySO.enemyType));

            SetEnemyPosition();
            while (Vector2.Distance(_currentEnemy.position, _playerTransform.position) < 2f)
            {
                SetEnemyPosition();
            }

            _currentEnemy.GetComponent<EnemyController>()
                .Initialize(new EnemyInitializationArgs(enemySO.enemyRange, enemySO.enemyDamage,
                    enemySO.enemyMovementSpeed, enemySO.health, enemySO.attackSpeed), enemySO.enemyType);

            _spawnerTimer = spawnRate;
            _currentEnemyCount++;
        }
    }

    private void SetEnemyPosition()
    {
        _currentEnemy.position = Utils.GetRandomPositionAtCertainPoint(_playerTransform.position, 6f);
    }

    public void EnemyDied(Vector2 diePosition)
    {
        OnEnemyDied?.Invoke(this, diePosition);
        _currentEnemyCount--;
    }
}