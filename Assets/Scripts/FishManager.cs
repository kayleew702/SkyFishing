﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private float timer;
    private float maxTimer;
    private Vector2 screenBounds;

    public int spawnY = 0;
    public Transform fishCollectible;
    public string spawningDown = "no";
    public string spawningUp = "no";
    public bool isReeling = false;
    public GameObject reelPosition;

    public float timerMin = 5f;
    public float timerMax = 12f;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        maxTimer = Random.Range(timerMin, timerMax);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        StartCoroutine("SpawnFishTimer");

    }

    private void Update()
    {
        spawnY = Random.Range(-4, 4);
        //Debug.Log(spawnY);
        if (spawningUp == "no")
        {
            spawningUp = "yes";
            StartCoroutine(SpawnFishTimer());
            isReeling = false;
        }

        //if the frog is reeling, stop spawning fish from bottom and start spawning them from top
        if (reelPosition && isReeling == true)
        {
            StopCoroutine(SpawnFishTimer());
            spawningDown = "yes";
            StartCoroutine(SpawnFishReelTimer());
        }
    }

    //Timer for how long an enemy will spawn
    IEnumerator SpawnFishTimer()
    {
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.5f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(fishCollectible, new Vector2(spawnY, -18), fishCollectible.rotation);
        spawningUp = "no";
    }

    IEnumerator SpawnFishReelTimer()
    {
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.2f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(fishCollectible, new Vector2(spawnY, 0), fishCollectible.rotation);
        spawningUp = "yes";
    }
}
