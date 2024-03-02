using UnityEngine;

public class BlowgunDart : MonoBehaviour, IUsable
{
    private Vector2 _initialPos;
    private Quaternion _initialRot;
    [SerializeField] private float dartForce = 3f;
    private Rigidbody2D _rb;
    private Transform _parent;
    private float _damage;

    private void Awake()
    {
        _initialPos = transform.localPosition;
        _initialRot = transform.localRotation;
        _parent = transform.parent;
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Use(float damage = 0)
    {
        transform.SetParent(_parent);
        gameObject.SetActive(true);
        transform.localPosition = _initialPos;
        transform.localRotation = _initialRot;
        transform.SetParent(null);
        _rb.velocity = Vector2.zero;
        _rb.AddForce(transform.right * dartForce, ForceMode2D.Impulse);
        _damage = damage;
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