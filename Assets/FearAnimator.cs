using UnityEngine;

public class FearAnimator : MonoBehaviour,IOneStageAnimatable
{
    private Animator _animator;
    private readonly string FEAR = "Fear";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Animate()
    {
        _animator.Play(FEAR);
    }
}
