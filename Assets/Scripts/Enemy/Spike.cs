public class Spike : EnemyController
{
    protected override void Attack()
    {
        _target.GetComponent<IDamagablePlayer>().GetDamage(_damage);
    }
}