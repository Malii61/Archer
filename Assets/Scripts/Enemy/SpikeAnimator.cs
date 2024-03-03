using System;
using UnityEngine;

public class SpikeAnimator : MonoBehaviour,IOneStageAnimatable
{
    private Animator _animator;
    private readonly string HIT = "Hit";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Animate()
    {
        _animator.Play(HIT);
    }
}
