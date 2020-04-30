using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCollectedController : MonoBehaviour
{
    public int fishCollected = 0;

    public void UpdateFishCollected()
    {
        GetComponent<Text>().text = "Fish Collected:  " + fishCollected;
    }
}
