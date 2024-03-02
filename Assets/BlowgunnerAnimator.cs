using UnityEngine;

public class BlowgunnerAnimator : MonoBehaviour, IOneStageAnimatable
{
    private readonly string HIT = "Hit";
    private readonly string SPEED = "Speed";
    private Animator _animator;
    private Blowgunner _blowgunner;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _blowgunner = GetComponentInParent<Blowgunner>();
    }

    private void Update()
    {
        _animator.SetInteger(SPEED, _blowgunner._currentSpeed);
    }

    public void Animate()
    {
        _animator.Play(HIT);
    }
}