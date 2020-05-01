using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private float timer;
    private float maxTimer;
    private Vector2 screenBounds;

    public int spawnY = 0;
    public Transform collectibleFish;
    public string spawning = "no";

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
        if (spawning == "no")
        {
            spawning = "yes";
            StartCoroutine(SpawnFishTimer());
        }
    }

    //Timer for how long a fish will spawn
    IEnumerator SpawnFishTimer()
    {
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.2f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(collectibleFish, new Vector2(spawnY, -18), collectibleFish.rotation);
        spawning = "no";
    }
}
