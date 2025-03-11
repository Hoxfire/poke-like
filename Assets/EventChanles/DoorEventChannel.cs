using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Door Event Channel", menuName = "Events/Door Event Channel")]
public class DoorEventChannel : ScriptableObject
{
    public UnityAction<string> OnDoorEntered;

    public void RaiseDoorEnteredEvent(string targetSceneName)
    {
        OnDoorEntered?.Invoke(targetSceneName);
    }
}
