using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    [Header("Channel")]
    [SerializeField] private DoorEventChannel doorEventChannel;

    [Header("Scene Stuff")]
    [SerializeField] private string targetSceneName;
    [SerializeField] private Vector2 newPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(InputController.instance.sceneTransition(targetSceneName,doorEventChannel,newPos));
        }
    }
}