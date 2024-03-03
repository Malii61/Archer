using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlineLobbyUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nickNameInputField;
    [SerializeField] private Button createRoomBtn, joinRoomBtn;
    private string onlineNickname;

    void Start()
    {
        createRoomBtn.onClick.AddListener(() => MenuSwitchScreenHandler.Instance.Open(Menu.CreateRoom));
        joinRoomBtn.onClick.AddListener(() => MenuSwitchScreenHandler.Instance.Open(Menu.FindRoom));
        onlineNickname = PlayerPrefs.GetString("onlineNick", "Player" + Random.Range(0, 999));
        nickNameInputField.text = onlineNickname;
        PhotonLauncher.Instance.ChangeRoomName(onlineNickname + "'s Room");
    }

    public void OnNicknameInputFieldChanged()
    {
        if (string.IsNullOrEmpty(nickNameInputField.text)) return;

        onlineNickname = nickNameInputField.text;
        SetNickname();
        PhotonLauncher.Instance.ChangeRoomName(onlineNickname + "'s Room");
    }

    private void SetNickname()
    {
        PhotonNetwork.LocalPlayer.NickName = onlineNickname;
        PlayerPrefs.SetString("onlineNick", onlineNickname);
    }
}