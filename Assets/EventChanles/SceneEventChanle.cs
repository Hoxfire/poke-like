using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Scene Event Channel", menuName = "Events/Scene Event Channel")]
public class SceneEventChanle : ScriptableObject
{
    public UnityAction<string> OnSceneRequested;

    public void RaiseEvent(string sceneName)
    {
        OnSceneRequested?.Invoke(sceneName);
    }
}
