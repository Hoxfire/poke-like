using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsingDoors : MonoBehaviour
{
    [SerializeField] private SceneEventChanle sceneEventChannel;

    public void ChangeScene(string newSceneName)
    {
        sceneEventChannel.RaiseEvent(newSceneName);
    }
}
