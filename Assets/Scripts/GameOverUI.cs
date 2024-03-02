using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private TextMeshProUGUI coinTMP, killedEnemyTMP, survivedTimeTMP;
    [SerializeField] private Button tryAgainBtn, backMenuBtn;
    private int coinCount, killedEnemyCount;

    private void Start()
    {
        GameManager.Instance.OnStageChanged += OnStageChanged;
        ItemDropManager.Instance.OnCoinCollected += OnCoinCollected;
        EnemySpawner.Instance.OnEnemyDied += OnEnemyDied;
        tryAgainBtn.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.GameScene));
        backMenuBtn.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.MenuScene));
        Hide();
    }

    private void OnCoinCollected(object sender, EventArgs e)
    {
        coinCount++;
    }

    private void OnEnemyDied(object sender, Vector2 e)
    {
        killedEnemyCount++;
    }

    private void OnStageChanged(object sender, GameManager.GameState e)
    {
        if (e == GameManager.GameState.GameOver)
        {
            Show();
        }
    }

    private void Hide()
    {
        content.gameObject.SetActive(false);
    }

    private void Show()
    {
        content.gameObject.SetActive(true);
        string survivedTimeText = Time.time < 60
            ? "00:" + Mathf.Round(Time.time)
            : (Time.time < 600
                ? "0" + Mathf.Round(Time.time / 60) + ":" + Mathf.Round(Time.time % 60)
                : Mathf.Round(Time.time / 60) + ":" + Mathf.Round(Time.time % 60));

        survivedTimeTMP.text = survivedTimeText;
        coinTMP.text = coinCount.ToString();
        killedEnemyTMP.text = killedEnemyCount.ToString();
    }
}