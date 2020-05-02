using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthController : MonoBehaviour
{
    public int currentDepth = 0;

    public int timeCurrent = 0;
    public int timeLength = 10;
    public bool meterReached = true;

    public bool frogPlayerDiving;
    public bool frogPlayerReeling;
    public bool reachedSurface;

    void FixedUpdate()
    {
        ReelingDepth();
        DivingDepth();
        CountSecond();
    }

    void CountSecond()
    {
        timeCurrent += 1;
        
        if (timeCurrent == timeLength)
        {
            meterReached = true;
            timeCurrent = 0;
        }

        else
        {
            meterReached = false;
        }
    }

    public void ReelingDepth()
    {
        frogPlayerReeling = GameObject.Find("frog").GetComponent<FrogController>().isReeling;
        reachedSurface = GameObject.Find("frog").GetComponent<FrogController>().reachedSurface;

        if ((frogPlayerReeling == true) && (meterReached == true))
        {
            currentDepth -= 1;
            GetComponent<Text>().text = "Depth:  " + currentDepth;
        }

        if (reachedSurface == true)
        {
            currentDepth = 0;
        }

        
    }

    public void DivingDepth()
    {

        if ((frogPlayerReeling == false) && (meterReached == true))
        {
            currentDepth += 1;
            GetComponent<Text>().text = "Depth:  " + currentDepth;
        }
    }
}
