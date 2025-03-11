using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEventListener : MonoBehaviour
{
    [SerializeField] private SceneEventChanle sceneEventChannel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sceneEventChannel != null && collision.gameObject.CompareTag("Player"))
        {
            sceneEventChannel.OnSceneRequested += LoadScene;
        }
    } 

    private void OnDisable()
    {
        if (sceneEventChannel != null)
        {
            sceneEventChannel.OnSceneRequested -= LoadScene;
        }
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
