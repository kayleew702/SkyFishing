using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingWaterFlip : MonoBehaviour
{
    public GameObject scrollingWater1;
    public GameObject scrollingWater2;
    public GameObject scrollingWater3;
    public float diveSpeed;
    public float reelSpeed;

    public GameObject frogPlayer;
    public bool frogIsReeling;
    public bool frogReachedSurface;

    public List<GameObject> scrollingWaterList;

    void Start()
    {
        scrollingWaterList.Add(scrollingWater1);
        scrollingWaterList.Add(scrollingWater2);
        scrollingWaterList.Add(scrollingWater3);
    }

    

    void Update()
    {
        GetBools();

        //if the frog is reeling, reverse the direction of the scrolling water
        if (frogIsReeling == true)
        {
            ReverseSpeed();
        }

        //if the frog has reached the surface, stop the scrolling water
        if (frogReachedSurface == true)
        {
            StopSpeed();
        }
    }


    void StopSpeed()
    {
        foreach (GameObject item in scrollingWaterList)
        {
            diveSpeed = item.GetComponent<BackgroundController>().scrollSpeed;
            item.GetComponent<BackgroundController>().scrollSpeed = .3f;
        }
    }

    public void GetBools()
    {
        //gets bools from the Frog Controller script
        frogIsReeling = frogPlayer.GetComponent<FrogController>().isReeling;
        frogReachedSurface = frogPlayer.GetComponent<FrogController>().reachedSurface;
    }

    public void ReverseSpeed()
    {
        //when the frog player is reeling, the scrolling water will move the opposite direction
        foreach (GameObject item in scrollingWaterList)
        {
            diveSpeed = item.GetComponent<BackgroundController>().scrollSpeed;
            item.GetComponent<BackgroundController>().scrollSpeed = reelSpeed;
        }
    }

    


}
