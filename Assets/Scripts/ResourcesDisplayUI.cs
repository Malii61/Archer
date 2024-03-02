using System;
using TMPro;
using UnityEngine;

public class ResourcesDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCountTMP, killedEnemyCountTMP;
    private int coinCount, killedEnemyCount;

    private void Start()
    {
        ItemDropManager.Instance.OnCoinCollected += OnCoinCollected;
        EnemySpawner.Instance.OnEnemyDied += OnEnemyDied;
    }

    private void OnEnemyDied(object sender, Vector2 e)
    {
        killedEnemyCount++;
        killedEnemyCountTMP.text = killedEnemyCount.ToString();
    }

    private void OnCoinCollected(object sender, EventArgs e)
    {
        coinCount++;
        coinCountTMP.text = coinCount.ToString();
    }
}