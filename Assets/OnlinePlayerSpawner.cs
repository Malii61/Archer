using System;
using System.Collections;
using System.IO;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class OnlinePlayerSpawner : MonoBehaviour
{
    private PhotonView PV;
    static GameObject controller;
    [SerializeField] private CinemachineVirtualCamera cm;
    private int joinedPlayerCount;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        PV.RPC(nameof(IncreaseJoinedPlayerCount), RpcTarget.All); // Synchronize joined player count across the network.
    }

    [PunRPC]
    private void IncreaseJoinedPlayerCount()
    {
        joinedPlayerCount++;
        // Check if the local player is the master client and there are at least 2 joined players.
        if (PhotonNetwork.IsMasterClient && joinedPlayerCount >= 2)
        {
            PV.RPC(nameof(CreateController), RpcTarget.All); // Synchronize controller creation across the network.
        }
    }

    [PunRPC]
    private void CreateController()
    {
        // Instantiate the player controller prefab and set the Cinemachine camera's follow target.
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "OnlineArcher"),
            Utils.GetRandomPositionAtCertainPoint(Vector2.zero, 2f), Quaternion.identity, 0,
            new object[] { PV.ViewID });
        cm.m_Follow = controller.transform;
    }
}