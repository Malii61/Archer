using UnityEngine;

public class Coin : Collectable
{
    public override void Collect()
    {
        ItemDropManager.Instance.CoinCollected();
        SoundManager.Instance.Play(Sound.Coin);
        base.Collect();
    }
}