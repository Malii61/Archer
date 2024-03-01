using UnityEngine;

public class EnemyController : MonoBehaviour, IFearable
{
    private Transform _target;
    private Rigidbody2D _rb;
    private float _attackRange;
    private float _damage;
    private float _moveSpeed;
    private float _attackSpeed;

    private float _attackTimer;

    //Fear
    private bool _isFeared;
    private float _fearDuration;
    private Transform _fearGhost;

    private void Awake()
    {
        _target = Player.Instance.transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(EnemyInitializationArgs initializationArgs, EnemyType enemyType)
    {
        _attackRange = initializationArgs.GetRange();
        _damage = initializationArgs.GetEnemyDamage();
        _moveSpeed = initializationArgs.GetMoveSpeed();
        _attackSpeed = initializationArgs.GetAttackSpeed();
        _attackTimer = _attackSpeed;
        GetComponent<EnemyHealthManager>().Initialize(initializationArgs.GetMaxHealth(), enemyType);
    }

    void Update()
    {
        CheckFear();
        if (_target != null)
        {
            _attackTimer -= Time.deltaTime;
            if (Vector2.Distance(transform.position, _target.position) <= _attackRange && !_isFeared)
            {
                if (_attackTimer <= 0)
                {
                    // Attack
                    _target.GetComponent<IDamagable>().GetDamage(_damage);
                    _attackTimer = _attackSpeed;
                }

                return;
            }

            var currentPosition = transform.position;
            Vector3 direction = _target.position - currentPosition;
            direction.Normalize();
            direction = _isFeared ? -direction : direction;
            Vector3 newPosition = currentPosition + direction * (_moveSpeed * Time.deltaTime);
            _rb.MovePosition(newPosition);
        }
    }

    private void CheckFear()
    {
        if (_isFeared)
        {
            _fearDuration -= Time.deltaTime;
            if (_fearDuration < 0)
            {
                _isFeared = false;
            }
        }
    }

    public void Fear(float duration)
    {
        _isFeared = true;
        _fearDuration = duration;
        _fearGhost = PoolHandler.Instance.Get(PoolType.FearGhost);
        _fearGhost.SetParent(transform);
        _fearGhost.localPosition = Vector2.zero;
        PoolHandler.Instance.Release(_fearGhost, PoolType.FearGhost, 1.5f);
    }
}

public class EnemyInitializationArgs
{
    private readonly float _attackRange;
    private readonly float _damage;
    private readonly float _moveSpeed;
    private readonly float _health;
    private readonly float _attackSpeed;

    public EnemyInitializationArgs(float attackRange, float damage, float moveSpeed, float health, float attackSpeed)
    {
        this._attackRange = attackRange;
        this._damage = damage;
        this._moveSpeed = moveSpeed;
        this._health = health;
        this._attackSpeed = attackSpeed;
    }

    public float GetRange()
    {
        return _attackRange;
    }

    public float GetEnemyDamage()
    {
        return _damage;
    }

    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }

    public float GetMaxHealth()
    {
        return _health;
    }

    public float GetAttackSpeed()
    {
        return _attackSpeed;
    }
}