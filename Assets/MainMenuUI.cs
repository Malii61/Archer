using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playBtn;

    void Start()
    {
        playBtn.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.GameScene));
    }
}