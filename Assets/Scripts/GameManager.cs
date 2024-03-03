using System;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler<GameState> OnStageChanged;
    [HideInInspector] public bool isGameStarted = false;
    [HideInInspector] public bool isGameOnline = false;
    private PhotonView PV;

    public enum GameState
    {
        Started,
        GameOver,
    }
    private void Awake()
    {
        Instance = this;
        if (TryGetComponent(out PhotonView _PV))
        {
            PV = _PV;
            isGameOnline = true;
        }
    }

    public void UpdateState(GameState state)
    {
        if (state == GameState.Started) isGameStarted = true;

        OnStageChanged?.Invoke(this, state);
    }
}