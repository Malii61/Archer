using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDropManager : MonoBehaviour
{
    public static ItemDropManager Instance { get; private set; }
    [SerializeField] private Collectable[] _collectables;
    [SerializeField] private float dropProbabilityPercent;
    public event EventHandler OnCoinCollected;
    public event EventHandler<float> OnFruitCollected;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var collectable in _collectables)
        {
            PoolHandler.Instance.Create(collectable.transform,
                PoolHandler.Instance.GetCollectablePoolType(collectable.collectableType));
        }

        EnemySpawner.Instance.OnEnemyDied += OnEnemyDied;
    }

    private void OnEnemyDied(object sender, Vector2 diePos)
    {
        var probability = Random.Range(0, 100);
        if (probability < dropProbabilityPercent)
        {
            var collectable = _collectables[Random.Range(0, _collectables.Length)];
            var collectableTransform =
                PoolHandler.Instance.Get(PoolHandler.Instance.GetCollectablePoolType(collectable.collectableType));
            collectableTransform.position = diePos;
        }
    }

    public void CoinCollected()
    {
        OnCoinCollected?.Invoke(this, EventArgs.Empty);
    }

    public void FruitCollected(float healAmount)
    {
        OnFruitCollected?.Invoke(this, healAmount);
    }
}