using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable, IReleasable
{
    internal float releaseDelay;
    public enum CollectableType
    {
        Coin,
        Fruit
    }

    public CollectableType collectableType;
    public virtual void Collect()
    {
        Release(releaseDelay);
    }
    public void Release(float releaseDeley = 0)
    {
        PoolHandler.Instance.Release(transform, PoolHandler.Instance.GetCollectablePoolType(collectableType),
            releaseDeley);
    }
}