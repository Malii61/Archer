using System;
using System.IO;
using Photon.Pun;
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
        if (!GameManager.Instance.isGameOnline)
            PoolHandler.Instance.Create(arrowPrefab, PoolType.Arrow);
    }

    public void Use(float damage = 0)
    {
        _animatable.Animate();
        Transform arrow;
        if (!GameManager.Instance.isGameOnline)
        {
            arrow = PoolHandler.Instance.Get(PoolType.Arrow);
        }
        else
        {
            arrow = PhotonNetwork
                .Instantiate(Path.Combine("PhotonPrefabs", "OnlineArrow"), Vector3.zero, Quaternion.identity)
                .transform;
        }
        arrow.rotation = transform.rotation;
        arrow.position = transform.position;
        arrow.GetComponent<Arrow>().Initialize(damage, 3, 5f);
    }
}