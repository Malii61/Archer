using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TMP_InputField nickNameInputField;
    [SerializeField] private Button saveNicknameBtn;
    [SerializeField] private List<HighscoreTableElement> _highscoreTableElements;
    [SerializeField] private Transform contentTransform;
    [SerializeField] private Button backBtn;
    private int maxHighscoreElementCount = 7;
    private const string NICKNAME_KEY = "nickname";
    private const string HIGHSCORES_KEY = "highscores";
    private string _nickname;
    private HighscoreArgs _highscoreArgs = new();
    private SingleHighscoreArg _lastSingleHighscoreArg = new();

    void Start()
    {
        _nickname = PlayerPrefs.GetString(NICKNAME_KEY, "Player789");
        nickNameInputField.text = _nickname;
        saveNicknameBtn.onClick.AddListener(SaveNickname);
        backBtn.onClick.AddListener(Hide);
        var highscoresJson = PlayerPrefs.GetString(HIGHSCORES_KEY);
        maxHighscoreElementCount = _highscoreTableElements.Count;
        if (!string.IsNullOrEmpty(highscoresJson))
        {
            _highscoreArgs = JsonHelper<HighscoreArgs>.Deserialize(highscoresJson);
            foreach (var VARIABLE in _highscoreArgs.args)
            {
                Debug.Log(VARIABLE.nickname);
            }
        }
        else
        {
            _highscoreArgs.args = new List<SingleHighscoreArg>();
        }

        Hide();
    }

    private void SaveNickname()
    {
        _nickname = nickNameInputField.text;
        PlayerPrefs.SetString(NICKNAME_KEY, _nickname);
        if (_lastSingleHighscoreArg != null)
        {
            _lastSingleHighscoreArg.nickname = _nickname;
        }

        _highscoreArgs.args.FirstOrDefault(x => x.id == _lastSingleHighscoreArg.id).nickname = _nickname;
        UpdateVisual();
    }

    private void Hide()
    {
        contentTransform.gameObject.SetActive(false);
    }

    public void Show()
    {
        contentTransform.gameObject.SetActive(true);
        _highscoreArgs.args.Add(new SingleHighscoreArg()
        {
            id = UnityEngine.Random.Range(0, 9999),
            nickname = _nickname,
            score = EnemySpawner.Instance.DiedEnemyCountTotal,
        });
        _lastSingleHighscoreArg = _highscoreArgs.args[^1];
        SortHighscoreArgs();
        UpdateVisual();
    }

    private void SortHighscoreArgs()
    {
        for (var i = 0; i < _highscoreArgs.args.Count; i++)
        {
            if (_highscoreArgs.args.Count < i + 1) break;
            for (var j = 0; j < i; j++)
            {
                if (_highscoreArgs.args[i].score > _highscoreArgs.args[j].score)
                {
                    (_highscoreArgs.args[i], _highscoreArgs.args[j]) = (_highscoreArgs.args[j], _highscoreArgs.args[i]);
                }
            }
        }
    }

    private void UpdateVisual()
    {
        HighscoreArgs highscoreArgs = new HighscoreArgs()
        {
            args = _highscoreArgs.args.GetRange(0, Math.Min(_highscoreArgs.args.Count, 8))
        };
        PlayerPrefs.SetString(HIGHSCORES_KEY, JsonHelper<HighscoreArgs>.Serialize(highscoreArgs));

        for (int i = 0; i < maxHighscoreElementCount; i++)
        {
            if (_highscoreArgs.args.Count < i + 1) break;

            HighscoreTableElement element = _highscoreTableElements[i];
            element.Initialize(i + 1, _highscoreArgs.args[i].score, _highscoreArgs.args[i].nickname);
        }

        for (var i = 0; i < Math.Min(_highscoreTableElements.Count, highscoreArgs.args.Count); i++)
        {
            _highscoreTableElements[i].gameObject.SetActive(true);
        }
    }
}

[Serializable]
public class HighscoreArgs
{
    public List<SingleHighscoreArg> args;
}

[Serializable]
public class SingleHighscoreArg
{
    public int id;
    public string nickname;
    public int score;
}