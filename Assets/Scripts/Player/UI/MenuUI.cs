using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuCurrsor;

    [Header("Menu Options")]
    [SerializeField] private int currsorMax;
    [SerializeField] private int currsorMin;
    [SerializeField] private Button[] buttons;

    private Vector2 currentPos;

    public void UpdateCursor(Vector2 newPos, menuState currentState) 
    {
        switch (currentState)
        {
            case menuState.None:
                break;
            case menuState.Menu:
                float newPosY = menuCurrsor.transform.localPosition.y + (newPos.y * 8);

                Debug.Log(newPosY);
                if (!(newPosY > currsorMax || newPosY < currsorMin))
                    currentPos = new Vector2(-24, newPosY);

                break;
            case menuState.playerInventory:
                break;
            case menuState.otherInventory:
                break;
        }

        menuCurrsor.transform.localPosition = currentPos;
    }
}
