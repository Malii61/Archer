using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CountdownUI countdownUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private Transform mobileInputTransform;

    void Start()
    {
        GameManager.Instance.OnStageChanged += OnStageChanged;
        mobileInputTransform.gameObject.SetActive(Application.platform == RuntimePlatform.Android);
        countdownUI.Show();
    }

    private void OnStageChanged(object sender, GameManager.GameState e)
    {
        if (e == GameManager.GameState.GameOver)
        {
            gameOverUI.Show();
        }
    }
}