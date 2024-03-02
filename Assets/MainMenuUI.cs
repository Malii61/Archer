using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button loginBtn, registerBtn;
    [SerializeField] private Transform menuTransform, loginTransform, registerTransform;
    [SerializeField] private Button loginBackBtn, registerBackBtn;

    void Start()
    {
        playBtn.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.GameScene));
        ADController.Instance.ShowBanner();
        loginBtn.onClick.AddListener(()=>ChangeLoginPanelState(true));
        loginBackBtn.onClick.AddListener(()=>ChangeLoginPanelState(false));
        registerBtn.onClick.AddListener(()=>ChangeRegisterPanelState(true));
        registerBackBtn.onClick.AddListener(()=>ChangeRegisterPanelState(false));
    }

    private void ChangeLoginPanelState(bool isActive)
    {
        menuTransform.gameObject.SetActive(!isActive);
        loginTransform.gameObject.SetActive(isActive);
        registerTransform.gameObject.SetActive(!isActive);
    }
    private void ChangeRegisterPanelState(bool isActive)
    {
        menuTransform.gameObject.SetActive(!isActive);
        loginTransform.gameObject.SetActive(!isActive);
        registerTransform.gameObject.SetActive(isActive);
    }
}