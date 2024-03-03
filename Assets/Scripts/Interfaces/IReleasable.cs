public interface IReleasable
{
    // Release the gameobject to the pool with an optional delay.
    void Release(float releaseDelay = 0f);
}