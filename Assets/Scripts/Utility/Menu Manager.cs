using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public MenuUI menuUI;
    public GameObject menuPanle;
    public menuState menuState;
    

    public void ToggleMenu() 
    {
        menuPanle.SetActive(!menuPanle.activeSelf);
        menuState = !menuPanle.activeSelf ? menuState=menuState.None : menuState=menuState.Menu;
    }
}

public enum menuState
{
    None,
    Menu,
    playerInventory,
    otherInventory
}