using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MenuArgs
{
    public Menu menu; // Enum representing the type of menu.
    public Transform menuTransform; // Transform of the associated menu.
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
        lastOpenScreen = menus.FirstOrDefault(x => x.menu == Menu.MainMenu).menuTransform; // Set the initial open screen to the main menu.
    }

    public void Open(Menu menu)
    {
        Transform t = menus.FirstOrDefault(x => x.menu == menu).menuTransform; // Get the Transform of the specified menu.
        t.gameObject.SetActive(true); 
        Close();
        lastOpenScreen = t; 
    }

    public void Close()
    {
        if (lastOpenScreen != null)
        {
            lastOpenScreen.gameObject.SetActive(false); 
        }
    }

    public void BackToOnlineLobby()
    {
        Open(Menu.OnlineLobby); 
    }
}