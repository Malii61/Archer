using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownTMP;
    [SerializeField] private int startValue = 3;

    public void Show()
    {
        InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);

    }
    private void UpdateCountdown()
    {
        startValue--;
        if (startValue == 0)
        {
            Invoke(nameof(DisableTMP), .75f);
            countdownTMP.text = "Start!";
            GameManager.Instance.UpdateState(GameManager.GameState.Started);
            return;
        }

        countdownTMP.text = startValue.ToString();
    }

    private void DisableTMP()
    {
        countdownTMP.enabled = false;
        CancelInvoke();
    }
}