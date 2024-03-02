using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDamagableEnemy
{
    private float _maxHealth;
    private float _health;
    private HealthBar _healthBar;
    private bool _isDied;
    private EnemyType _enemyType;
    private IOneStageAnimatable hitAnimatable;

    private void Awake()
    {
        _healthBar = GetComponentInChildren<HealthBar>();
        hitAnimatable = GetComponentInChildren<IOneStageAnimatable>();
    }

    public void Initialize(float health, EnemyType enemyType)
    {
        _maxHealth = health;
        _health = _maxHealth;
        _enemyType = enemyType;
        _isDied = false;
        _healthBar.SetHealth(_health, _maxHealth);
    }

    public void GetDamage(float damage)
    {
        if (_isDied) return;

        _health -= damage;
        _healthBar.SetHealth(_health, _maxHealth);
        ParticleManager.Instance.Play(ParticleType.Blood, transform.position, Quaternion.identity, 1f);
        hitAnimatable?.Animate();
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDied = true;
        EnemySpawner.Instance.EnemyDied(transform.position);
        PoolHandler.Instance.Release(transform, PoolHandler.Instance.GetEnemyPoolType(_enemyType));
    }
}