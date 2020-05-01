using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderTriggerNewScene : MonoBehaviour
{
    public bool playButton;
    public bool quitButton;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (playButton == true)
        {
            SceneManager.LoadScene("Main Level");
            Debug.Log("Main Level Loaded");
        }

        else if (quitButton == true)
        {
            Application.Quit();
        }
    }
}
