using UnityEngine;
public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _movementSpeed;
    [SerializeField] private PlayerAnimator _animator;
    private float _speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _movementSpeed = Player.Instance.GetPlayerMoveSpeed();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        var moveAmount = GameInput.Instance.GetMovementVectorNormalized() * (_movementSpeed * Time.deltaTime);
        _animator.SetLookingDirection(moveAmount.x);
        _animator.SetSpeed(moveAmount.magnitude);
        rb.MovePosition(rb.position + moveAmount);
    }
}