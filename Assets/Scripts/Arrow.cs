using System;
using UnityEngine;

public class Arrow : MonoBehaviour, IReleasable
{
    private Rigidbody2D rb;
    private bool _isReleased;
    private float _damage;
    private bool _isTriggered;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float damage, float forceAmount, float lifeTime)
    {
        _isReleased = false;
        _damage = damage;
        _isTriggered = false;
        Invoke(nameof(Release), lifeTime);
        rb.AddForce(transform.right * forceAmount, ForceMode2D.Impulse);
    }

    public void Release()
    {
        if (_isReleased) return;

        PoolHandler.Instance.Release(transform, PoolType.Arrow);
        _isReleased = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_isTriggered) return;
        
        if (other.transform.TryGetComponent(out IDamagableEnemy iDamagableEnemy))
        {
            _isTriggered = true;
            iDamagableEnemy.GetDamage(_damage);
            Release();
        }
    }
}