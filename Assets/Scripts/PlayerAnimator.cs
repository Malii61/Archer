using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private const string SPEED = "Speed";
    private const string HIT = "PlayerHit";
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Player.Instance.OnPlayerHit += OnPlayerHit;
    }

    private void OnPlayerHit(object sender, EventArgs e)
    {
        _animator.Play(HIT);
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(SPEED, speed);
    }

    public void SetLookingDirection(float xVal)
    {
        _spriteRenderer.flipX = xVal < 0;
    }
}