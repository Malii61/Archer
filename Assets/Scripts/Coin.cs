using UnityEngine;

public class Coin : Collectable
{
    public override void Collect()
    {
        ItemDropManager.Instance.CoinCollected();
        base.Collect();
    }
}