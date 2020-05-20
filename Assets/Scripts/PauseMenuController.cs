using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    GameObject[] pauseObjects;
    private int currentScore;
    private int currenthighScore;
    private Text currentScoreText;
    private Text currenthighScoreText;

    public bool menuActivated;

    void UpdateScore()
    {
        GetScore();
        currenthighScoreText.text = "High Score: " + currenthighScore;
        currentScoreText.text = "Fish Collected: " + currentScore;
    }

    void GetScore()
    {
        currentScore = GameObject.Find("FishCollectedText").GetComponent<FishCollectedController>().fishCollected;
        currenthighScore = GameObject.Find("HighScoreText").GetComponent<HighScoreController>().highScore;
        currentScoreText = GameObject.Find("CurrentScoreText").GetComponent<Text>();
        currenthighScoreText = GameObject.Find("CurrentHighScoreText").GetComponent<Text>();
    }

    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPaused");
        DeactivateMenu();

        menuActivated = false;
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
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void ActivateMenu()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
            UpdateScore();
        }
        menuActivated = true;
    }

    public void DeactivateMenu()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        Time.timeScale = 1;
        menuActivated = false;
    }
}
