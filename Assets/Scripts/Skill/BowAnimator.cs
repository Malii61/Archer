using UnityEngine;

public class BowAnimator : MonoBehaviour, IOneStageAnimatable
{
    private Animator _animator;
    private readonly string ATTACK = "Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Animate()
    {
        _animator.Play(ATTACK);
    }
}