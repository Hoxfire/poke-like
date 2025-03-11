using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private DoorEventChannel doorEventChannel;

    private void OnEnable()
    {
        if (doorEventChannel != null)
        {
            doorEventChannel.OnDoorEntered += LoadScene;
        }
    }

    private void OnDisable()
    {
        if (doorEventChannel != null)
        {
            doorEventChannel.OnDoorEntered -= LoadScene;
        }
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}