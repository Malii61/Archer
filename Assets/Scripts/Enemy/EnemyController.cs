using UnityEngine;

public abstract class EnemyController : MonoBehaviour, IFearable
{
    protected Transform _target;

    private Rigidbody2D _rb;

    private float _attackRange;
    protected float _damage;
    private float _moveSpeed;
    private float _attackSpeed;

    private float _attackTimer;

    // Fear related variables
    private bool _isFeared;
    private float _fearDuration;
    private Transform _fearGhost;

    internal int _currentSpeed;

    private float _walkableTimer;
    private readonly float _walkableTimerMax = 0.5f;

    private void Awake()
    {
        _target = Player.Instance.transform;

        _rb = GetComponent<Rigidbody2D>();
    }

    // Initialization method for setting up enemy parameters
    public void Initialize(EnemyInitializationArgs initializationArgs, EnemyType enemyType)
    {
        // Set up enemy parameters
        _attackRange = initializationArgs.GetRange();
        _damage = initializationArgs.GetEnemyDamage();

        // Adjust move speed based on the platform (Android)
        _moveSpeed = initializationArgs.GetMoveSpeed();
        _moveSpeed = Application.platform == RuntimePlatform.Android ? _moveSpeed / 5f : _moveSpeed;

        _attackSpeed = initializationArgs.GetAttackSpeed();
        _attackTimer = _attackSpeed;

        // Initialize enemy health manager with max health and enemy type
        GetComponent<EnemyHealthManager>().Initialize(initializationArgs.GetMaxHealth(), enemyType);

        _rb.velocity = Vector2.zero;
    }

    // Update method is called once per frame
    private void Update()
    {
        // Check for fear effect
        CheckFear();

        // Proceed with normal behavior if there's a target (player)
        if (_target != null)
        {
            _attackTimer -= Time.deltaTime;

            // Check if the player is within attack range and not feared
            if (Vector2.Distance(transform.position, _target.position) <= _attackRange && !_isFeared)
            {
                // Attack if cooldown is over
                if (_attackTimer <= 0)
                {
                    Attack();
                    _attackTimer = _attackSpeed;
                }

                // Reset walkable timer and current speed
                _walkableTimer = _walkableTimerMax;
                _currentSpeed = 0;
                return;
            }

            // Check if the walkable timer is still active and not feared
            if (_walkableTimer > 0 && !_isFeared)
            {
                _walkableTimer -= Time.deltaTime;
                return;
            }

            // Calculate movement direction and move towards the player
            var currentPosition = transform.position;
            Vector3 direction = _target.position - currentPosition;
            direction.Normalize();
            direction = _isFeared ? -direction : direction;
            _currentSpeed = _isFeared ? -1 : 1;

            // Move towards the target position
            Vector3 newPosition = currentPosition + direction * (_moveSpeed * Time.deltaTime);
            _rb.MovePosition(newPosition);
        }
    }

    // Abstract method for handling enemy attacks (to be implemented by subclasses)
    protected abstract void Attack();

    // Check and handle the fear effect duration
    private void CheckFear()
    {
        if (_isFeared)
        {
            _fearDuration -= Time.deltaTime;

            // Disable fear effect if the duration is over
            if (_fearDuration < 0)
            {
                _isFeared = false;
            }
        }
    }

    // Apply the fear effect to the enemy
    public void Fear(float duration)
    {
        _isFeared = true;
        _fearDuration = duration;

        // Create and position the fear ghost from pool
        _fearGhost = PoolHandler.Instance.Get(PoolType.FearGhost);
        _fearGhost.SetParent(transform);
        _fearGhost.localPosition = Vector2.zero;

        // Release the fear ghost back to the pool after a certain delay
        PoolHandler.Instance.Release(_fearGhost, PoolType.FearGhost, 1.5f);
    }
}

// Class to hold initialization parameters for enemies
public class EnemyInitializationArgs
{
    private readonly float _attackRange;
    private readonly float _damage;
    private readonly float _moveSpeed;
    private readonly float _health;
    private readonly float _attackSpeed;

    // Constructor to set up enemy parameters
    public EnemyInitializationArgs(float attackRange, float damage, float moveSpeed, float health, float attackSpeed)
    {
        this._attackRange = attackRange;
        this._damage = damage;
        this._moveSpeed = moveSpeed;
        this._health = health;
        this._attackSpeed = attackSpeed;
    }

    // Getter methods for accessing enemy parameters
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
