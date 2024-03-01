using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public enum CollectableType
    {
        Coin,
        Fruit
    }

    public CollectableType collectableType;

    public virtual void Collect()
    {
        PoolHandler.Instance.Release(transform, PoolHandler.Instance.GetCollectablePoolType(collectableType));
    }
}