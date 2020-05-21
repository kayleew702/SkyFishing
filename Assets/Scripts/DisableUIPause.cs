using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUIPause : MonoBehaviour
{
    public GameObject FishCollectedText;
    public GameObject DepthMeter;
    public GameObject HighScoreText;
    private List<GameObject> textList;

    public bool isPaused;
    public bool bigScore;
    public bool uiDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        textList.Add(FishCollectedText);
        textList.Add(DepthMeter);
        textList.Add(HighScoreText);

        isPaused = false;
        bigScore = false;
        //uiDisplayed = true;
    }

    // Update is called once per frame
    void Update()
    {
        //disable on pause
        isPaused = GameObject.Find("Pause Menu Controller").GetComponent<PauseMenuController>().menuActivated;
        if (isPaused == true && uiDisplayed == true)
        {
            uiDisplayed = false;
            foreach (GameObject text in textList)
            {
                //text.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                text.SetActive(false);
                Debug.Log("ui turned off");
            }
            
        }
        if (isPaused == false && uiDisplayed == false)
        {
            uiDisplayed = true;
            foreach (GameObject text in textList)
            {
                //text.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
                text.SetActive(true);
                Debug.Log("ui turned on");
            }
            
        }



        //disable when bigScore shows
        bigScore = GameObject.Find("Pause Menu Controller").GetComponent<PauseMenuController>().bigScoreDisplaying;

    }
}
