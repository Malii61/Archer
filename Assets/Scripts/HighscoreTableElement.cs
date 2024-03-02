using TMPro;
using UnityEngine;

public class HighscoreTableElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI indexTMP, nicknameTMP, scoreTMP;

    public void Initialize(int index, int score, string nickname)
    {
        nicknameTMP.text = nickname;
        indexTMP.text = index.ToString();
        scoreTMP.text = score.ToString();
    }
}