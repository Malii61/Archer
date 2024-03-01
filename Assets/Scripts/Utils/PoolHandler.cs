using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum PoolType
{
    Arrow,

    // Enemies
    Spike,

    //Collectables
    Coin,
    Fruit,

    //Skill
    Fear,
    Shurikens,
    
    //Skill Effects
    FearGhost,
}

public class PoolHandler : MonoBehaviour
{
    public static PoolHandler Instance;
    private Dictionary<PoolType, ObjectPool<Transform>> _pools = new();

    private void Awake()
    {
        Instance = this;
    }

    public void Create(Transform prefab, PoolType poolType, int initial = 10, int max = 20,
        bool collectionChecks = false)
    {
        if (_pools.ContainsKey(poolType))
        {
            if (_pools[poolType].Get() == prefab)
                return;

            _pools[poolType].Dispose();
        }

        var pool = new ObjectPool<Transform>(
            () => { return Instantiate(prefab); },
            GetSetup,
            ReleaseSetup,
            DestroySetup,
            collectionChecks,
            initial,
            max);

        _pools[poolType] = pool;
    }

    public Transform Get(PoolType poolType) => !_pools.ContainsKey(poolType) ? null : _pools[poolType].Get();

    public void Release(Transform prefab, PoolType poolType, float delay = 0f)
    {
        if (prefab is null) return;

        var _delay = new WaitForSeconds(delay);
        StartCoroutine(Release(prefab, poolType, _delay));
    }

    private IEnumerator Release(Transform prefab, PoolType poolType, WaitForSeconds delay)
    {
        yield return delay;
        prefab.SetParent(null);
        _pools[poolType].Release(prefab);
    }

    public PoolType GetEnemyPoolType(EnemyType enemyType)
    {
        switch (enemyType)
        {
            default:
            case EnemyType.Spike:
                return PoolType.Spike;
        }
    }

    public PoolType GetCollectablePoolType(Collectable.CollectableType collectableType)
    {
        switch (collectableType)
        {
            default:
            case Collectable.CollectableType.Coin:
                return PoolType.Coin;
            case Collectable.CollectableType.Fruit:
                return PoolType.Fruit;
        }
    }

    public PoolType GetSkillPoolType(SkillSO.SkillId skillId)
    {
        return skillId switch
        {
            SkillSO.SkillId.Fear => PoolType.Fear,
            SkillSO.SkillId.Shurikens => PoolType.Shurikens,
            _ => PoolType.Fear
        };
    }

    protected virtual void GetSetup(Transform obj) => obj.gameObject.SetActive(true);
    protected virtual void ReleaseSetup(Transform obj) => obj.gameObject.SetActive(false);
    protected virtual void DestroySetup(Transform obj) => Destroy(obj.gameObject);
}