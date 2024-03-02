using System;
using UnityEngine;

public class Arrow : MonoBehaviour, IReleasable
{
    private Rigidbody2D rb;
    private bool _isReleased;
    private float _damage;
    private bool _isTriggered;
    private float _lifetime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float damage, float forceAmount, float lifeTime)
    {
        _isReleased = false;
        _damage = damage;
        _isTriggered = false;
        _lifetime = lifeTime;
        rb.AddForce(transform.right * forceAmount, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (_lifetime > 0)
        {
            _lifetime -= Time.deltaTime;
            return;
        }
        Release();
    }

    public void Release(float releaseDelay = 0f)
    {
        if (_isReleased) return;

        PoolHandler.Instance.Release(transform, PoolType.Arrow);
        _isReleased = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isTriggered) return;

        if (other.transform.TryGetComponent(out IDamagableEnemy iDamagableEnemy))
        {
            _isTriggered = true;
            iDamagableEnemy.GetDamage(_damage);
            Release();
        }
    }
}