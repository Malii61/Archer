using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler<GameState> OnStageChanged;
    public enum GameState
    {
        Playing,
        GameOver,
    }
    private void Awake()
    {
        Instance = this;
    }
    public void UpdateState(GameState state)
    {
        OnStageChanged?.Invoke(this, state);
    }
}