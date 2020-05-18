using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    GameObject[] pauseObjects;

    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPaused");
        DeactivateMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0f;
                ActivateMenu();
            }
            else if (Time.timeScale == 0f)
            {
                Debug.Log("No longer paused");
                Time.timeScale = 1;
                DeactivateMenu();
            }
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ActivateMenu()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void DeactivateMenu()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        Time.timeScale = 1;
    }
}
