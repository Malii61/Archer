using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    public static PhotonLauncher Instance;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] GameObject startGameButton;

    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        Instance = this; 
    }

    public void Connect()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings(); // Connect to Photon Master server.
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby(); // Join the lobby after connecting to the Master server.
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuSwitchScreenHandler.Instance.Open(Menu.OnlineLobby); // Open Online Lobby menu on successful lobby join.
    }

    public void ChangeRoomName(string name)
    {
        roomNameInputField.text = name; // Update room name input field.
    }

    public void CreateRoom()
    {
        Photon.Realtime.RoomOptions ropts = new Photon.Realtime.RoomOptions()
            { IsOpen = true, IsVisible = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom(roomNameInputField.text, ropts); // Create a room with specified options.
        MenuSwitchScreenHandler.Instance.Open(Menu.Loading);
    }

    public override void OnJoinedRoom()
    {
        MenuSwitchScreenHandler.Instance.Open(Menu.OnlineGameRoom); // Open Online Game Room menu on successful room join.
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        var players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var pl in players)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(pl);
        }
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient); // Activate start game button for the master client.
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message; 
        Debug.LogError("Room Creation Failed: " + message);
        MenuSwitchScreenHandler.Instance.Open(Menu.Error); 
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(2); // Load the game scene.
    }

    public void LeaveLobby()
    {
        PhotonNetwork.Disconnect(); // Disconnect from Photon.
        MenuSwitchScreenHandler.Instance.Open(Menu.Loading); 
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        MenuSwitchScreenHandler.Instance.Open(Menu.MainMenu); // Open Main Menu on disconnection.
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(); 
        MenuSwitchScreenHandler.Instance.Open(Menu.Loading); 
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuSwitchScreenHandler.Instance.Open(Menu.Loading); 
    }

    // Commented out code, needs further context to provide appropriate comments.
    // public override void OnLeftRoom()
    // {
    //     MenuSwitchScreenHandler.Instance.Open(Menu.OnlineLobby);
    // }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            RoomListItem roomItem = Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>();
            roomItem.SetUp(roomList[i]);
            roomItem.playerCount.text = roomList[i].PlayerCount + "/" + roomList[i].MaxPlayers;
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        errorText.text = "Joined room Failed: " + message; // Display an error message on room join failure.
        MenuSwitchScreenHandler.Instance.Open(Menu.Error); // Open Error menu.
    }
}
