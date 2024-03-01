using System;
using UnityEngine;

public class SingleShuriken : MonoBehaviour
{
    private Vector2 _initialPosition;
    private Quaternion _initialRotation;
    private Rigidbody2D _rb;
    private float _damage;
    private Transform _parent;

    private void Awake()
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
        _parent = transform.parent;
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Throw(float force, float hitDamage)
    {
        transform.SetParent(_parent);
        gameObject.SetActive(true);
        transform.localPosition = _initialPosition;
        transform.localRotation = _initialRotation;
        transform.SetParent(null);
        _rb.velocity = Vector2.zero;
        _rb.AddForce(transform.right * force, ForceMode2D.Impulse);
        _damage = hitDamage;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable iDamagable))
        {
            if (other.TryGetComponent(out Player player)) return;

            Debug.Log("hit");
            iDamagable.GetDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}