using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{

    public int highScore;
    private Text fishCollectedText;
    private bool reachedSurface;


    // Start is called before the first frame update
    void Start()
    {
        reachedSurface = false;

        //highScore saved between games as player preference
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        //and it's written to the high score game object that the script is attached to
        GetComponent<Text>().text = "High Score:  " + highScore.ToString();

        fishCollectedText = GameObject.Find("FishCollectedText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        reachedSurface = GameObject.Find("frog").GetComponent<FrogController>().reachedSurface;
        //score will update if frog has returned to the starting point and has caught enough fish

        //score from fishCollectedController
        int fishCollected = fishCollectedText.GetComponent<FishCollectedController>().fishCollected;
        if ((fishCollected > highScore) && reachedSurface == true)
        {
            //update high score
            highScore = fishCollected;
            PlayerPrefs.SetInt("HighScore", fishCollected);
            GetComponent<Text>().text = "HighScore:  " + fishCollected;
        }
    }
}
