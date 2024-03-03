using Photon.Pun;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _movementSpeed;
    [SerializeField] private PlayerAnimator _animator;

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
        // Check if the game is online and if the player object belongs to the local player.
        if (GameManager.Instance.isGameOnline)
        {
            if (!GetComponent<PhotonView>().IsMine)
                return; // Do nothing if the player object is not controlled by the local player.
        }
        Move(); 
    }

    private void Move()
    {
        var moveAmount = GameInput.Instance.GetMovementVectorNormalized() * (_movementSpeed * Time.deltaTime);
        _animator.SetLookingDirection(moveAmount.x); // Set the looking direction for animation.
        _animator.SetSpeed(moveAmount.magnitude);
        rb.MovePosition(rb.position + moveAmount); 
    }
}