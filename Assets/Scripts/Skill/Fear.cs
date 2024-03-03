using System;
using UnityEngine;

public class Fear : MonoBehaviour, IUsable
{
    [SerializeField] private float fearDuration;
    [SerializeField] private Transform fearGhostPrefab;
    private IOneStageAnimatable _animatable;

    private void Awake()
    {
        _animatable = GetComponent<IOneStageAnimatable>();
    }

    private void Start()
    {
        PoolHandler.Instance.Create(fearGhostPrefab, PoolType.FearGhost);
    }

    public void Use(float damage = 0)
    {
        _animatable.Animate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IFearable fearable))
        {
            fearable.Fear(fearDuration);
        }
    }
}