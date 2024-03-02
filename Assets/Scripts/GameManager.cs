using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler<GameState> OnStageChanged;
    public bool isGameStarted = false;
    public enum GameState
    {
        Started,
        GameOver,
    }
    private void Awake()
    {
        Instance = this;
    }
    public void UpdateState(GameState state)
    {
        if (state == GameState.Started) isGameStarted = true;
        
        OnStageChanged?.Invoke(this, state);
    }
}