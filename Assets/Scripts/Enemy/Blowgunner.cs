using UnityEngine;

public class Blowgunner : EnemyController
{
    [SerializeField] private BlowgunDart _blowgunDart;
    protected override void Attack()
    {
        _blowgunDart.Use(_damage);
    }
}