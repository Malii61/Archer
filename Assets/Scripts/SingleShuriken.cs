using System;
using UnityEngine;

public class SingleShuriken : MonoBehaviour,IUsable
{
    private Vector2 _initialPosition;
    private Quaternion _initialRotation;
    private Rigidbody2D _rb;
    private float _damage;
    private Transform _parent;
    [SerializeField] private float forceAmount;
    private void Awake()
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
        _parent = transform.parent;
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Use(float damage = 0)
    {
        transform.SetParent(_parent);
        gameObject.SetActive(true);
        transform.localPosition = _initialPosition;
        transform.localRotation = _initialRotation;
        transform.SetParent(null);
        _rb.velocity = Vector2.zero;
        _rb.AddForce(transform.right * forceAmount, ForceMode2D.Impulse);
        _damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagableEnemy iDamagableEnemy))
        {
            iDamagableEnemy.GetDamage(_damage);
            gameObject.SetActive(false);
        }
    }

 
}