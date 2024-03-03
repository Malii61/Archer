using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MenuArgs
{
    public Menu menu;
    public Transform menuTransform;
}

public enum Menu
{
    MainMenu,
    Login,
    Register,
    OnlineLobby,
    Loading,
    OnlineGameRoom,
    CreateRoom,
    FindRoom,
    Error,
}

public class MenuSwitchScreenHandler : MonoBehaviour
{
    public static MenuSwitchScreenHandler Instance { get; private set; }
    [SerializeField] private List<MenuArgs> menus = new();
    private Transform lastClosedScreen;

    private void Awake()
    {
        Instance = this;
    }

    private Transform lastOpenScreen;

    private void Start()
    {
        lastOpenScreen = menus.FirstOrDefault(x => x.menu == Menu.MainMenu).menuTransform;
    }

    public void Open(Menu menu)
    {
        Debug.Log("opened" + menu.ToString());
        Transform t = menus.FirstOrDefault(x => x.menu == menu).menuTransform;
        t.gameObject.SetActive(true);
        Close();
        lastOpenScreen = t;
    }

    public void Close()
    {
        if (lastOpenScreen != null)
        {
            lastOpenScreen.gameObject.SetActive(false);
            Debug.Log("closed" + lastOpenScreen.name);
        }
    }

    public void BackToOnlineLobby()
    {
        Open(Menu.OnlineLobby);
    }
}