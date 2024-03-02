using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button loginBtn, registerBtn;
    [SerializeField] private Button loginBackBtn, registerBackBtn;

    void Start()
    {
        playBtn.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.GameScene));
        ADController.Instance.ShowBanner();
        loginBtn.onClick.AddListener(() => MenuSwitchScreenHandler.Instance.Open(Menu.Login));
        loginBackBtn.onClick.AddListener(() => MenuSwitchScreenHandler.Instance.Open(Menu.MainMenu));
        registerBtn.onClick.AddListener(() => MenuSwitchScreenHandler.Instance.Open(Menu.Register));
        registerBackBtn.onClick.AddListener(() => MenuSwitchScreenHandler.Instance.Open(Menu.MainMenu));
    }

}