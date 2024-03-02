using UnityEngine;

public class Fruit : Collectable
{
    [SerializeField] private float healAmount = 10f;
    private readonly string COLLECTED = "Collected";
    public override void Collect()
    {
        ItemDropManager.Instance.FruitCollected(healAmount);
        GetComponent<Animator>().Play(COLLECTED);
        releaseDelay = .5f;
        base.Collect();
    }
}