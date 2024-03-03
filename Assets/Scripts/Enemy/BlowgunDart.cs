using System;
using UnityEngine;

public class BlowgunDart : MonoBehaviour, IUsable
{
    [SerializeField] private float dartForce = 3f;
    private Rigidbody2D _rb;
    private Transform _parent;
    private float _damage;

    private void Awake()
    {
        _parent = transform.parent;
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
    }


    public void Use(float damage = 0)
    {
        gameObject.SetActive(true);
        transform.SetParent(_parent);
        ResetPosition();
        transform.SetParent(null);
        _rb.isKinematic = false;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(transform.right * dartForce, ForceMode2D.Impulse);
        _damage = damage;
    }

    private void ResetPosition()
    {
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagablePlayer damagablePlayer))
        {
            damagablePlayer.GetDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}