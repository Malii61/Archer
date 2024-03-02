using UnityEngine;
public class Fruit : Collectable
{
    [SerializeField] private float healAmount = 10f;

    public override void Collect()
    {
        ItemDropManager.Instance.FruitCollected(healAmount);
        base.Collect();
    }
}