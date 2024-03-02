using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
     [SerializeField] private Transform statisticsContent;
    [SerializeField] private TextMeshProUGUI coinTMP, killedEnemyTMP, survivedTimeTMP;
    [SerializeField] private Button tryAgainBtn, backMenuBtn,showLeaderboardBtn;
    private int coinCount, killedEnemyCount;
    [SerializeField] private TextMeshProUGUI youDiedTMP;
    private void Start()
    {
        ItemDropManager.Instance.OnCoinCollected += OnCoinCollected;
        EnemySpawner.Instance.OnEnemyDied += OnEnemyDied;
        tryAgainBtn.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.GameScene));
        backMenuBtn.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.MenuScene));
        showLeaderboardBtn.onClick.AddListener(() => UIManager.Instance.ShowLeaderboard());
        HideStatictics();
    }

    private void OnCoinCollected(object sender, EventArgs e)
    {
        coinCount++;
    }

    private void OnEnemyDied(object sender, Vector2 e)
    {
        killedEnemyCount++;
    }
    public void Show()
    {
        youDiedTMP.enabled = true;
        Invoke(nameof(ShowStatictics),1.5f);
    }
    private void HideStatictics()
    {
        statisticsContent.gameObject.SetActive(false);
    }

    private void ShowStatictics()
    {
        // ADController.Instance.ShowInterstitial();
        youDiedTMP.enabled = false;
        statisticsContent.gameObject.SetActive(true);
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