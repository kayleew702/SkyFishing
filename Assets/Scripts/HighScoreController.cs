using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{

    public int highScore = 0;
    private int fishCollected;
    public Text highScoreText;
    private Text fishCollectedText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.text = highScore.ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        fishCollectedText = GameObject.Find("FishCollectedText").GetComponent<Text>();
    }

    // Update is called once per frame
    public void UpdateHighScore()
    {
        fishCollectedText.GetComponent<FishCollectedController>().fishCollected = 0;
        fishCollected = 0;
        fishCollectedText.GetComponent<FishCollectedController>().UpdateFishCollected();

        if (fishCollected > highScore)
        {
            highScore = fishCollected;

            highScoreText.text = highScore.ToString();

            PlayerPrefs.SetInt("highScore", highScore);
        }
    }
}
