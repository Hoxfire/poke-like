using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private string newScene;
    [SerializeField] private Vector2 exitPos;

    [Header("Channles")]
    [SerializeField] private SceneEventChanle SceneEventChanle;

}
