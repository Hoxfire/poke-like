using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsingDoors : MonoBehaviour
{
    [Header("Chanles")]
    [SerializeField] private Scene newScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneManager.LoadScene(newScene.name);
    }
    /*
    IEnumerator SceneTransition()
    {
        
    }
    */
}
