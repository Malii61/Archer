using System;
using UnityEngine;

public class Bow : MonoBehaviour, IUsable
{
    private IOneStageAnimatable _animatable;
    [SerializeField] private Transform arrowPrefab;

    private void Awake()
    {
        _animatable = GetComponent<IOneStageAnimatable>();
    }

    private void Start()
    {
        PoolHandler.Instance.Create(arrowPrefab, PoolType.Arrow);
    }

    public void Use(float damage = 0)
    {
        _animatable.Animate();
        var arrow = PoolHandler.Instance.Get(PoolType.Arrow);
        arrow.rotation = transform.rotation;
        arrow.position = transform.position;
        arrow.GetComponent<Arrow>().Initialize(damage, 3, 5f);
    }
}