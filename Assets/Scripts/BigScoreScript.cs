using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BigScoreScript : MonoBehaviour
{
    private GameObject bigScore = GameObject.Find("BigScore");

    private int highScore;
    private Text highScoreBig;
    private int fishScore;
    private Text fishScoreBig;

    public bool bigScoreDisplaying;

    void Start()
    {
        bigScoreDisplaying = false;
    }
    public void ShowScore()
    {
        GetScore();
        highScoreBig.text = "High Score: " + highScore;
        fishScoreBig.text = "Fish Collected: " + fishScore;
        Debug.Log("Show Score works");
        bigScoreDisplaying = true;

    }

    public void GetScore()
    {
        fishScore = GameObject.Find("FishCollectedText").GetComponent<FishCollectedController>().fishCollected;
        highScoreBig = GameObject.Find("HighScoreBig").GetComponent<Text>();
        highScore = GameObject.Find("HighScoreText").GetComponent<HighScoreController>().highScore;
        fishScoreBig = GameObject.Find("FishCollectedBig").GetComponent<Text>();
        Debug.Log("change big score");
    }
}
