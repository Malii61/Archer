using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private CountdownUI countdownUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private Transform mobileInputTransform;
    [SerializeField] private Leaderboard _leaderboard;

    private void Awake()
    {
        Instance = this; 
    }

    void Start()
    {
        GameManager.Instance.OnStageChanged += OnStageChanged; 
        mobileInputTransform.gameObject.SetActive(Application.platform == RuntimePlatform.Android); // Activate mobile UI on Android.
        countdownUI.Show();
    }

    public void ShowLeaderboard()
    {
        _leaderboard.Show(); // Display the leaderboard.
    }

    private void OnStageChanged(object sender, GameManager.GameState e)
    {
        if (e == GameManager.GameState.GameOver)
        {
            gameOverUI.Show(); 
        }
    }
}