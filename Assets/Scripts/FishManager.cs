using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private float timer;
    private float maxTimer;
    private Vector2 screenBounds;

    public int spawnY = 0;
    public Transform fishCollectible;
    public bool spawning = false;
    public bool isReeling = GameObject.Find("frog").GetComponent<FrogController>().isReeling;
    public float speed = GameObject.Find("Fish").GetComponent<FishController>().speed;

    public float timerMin = 5f;
    public float timerMax = 12f;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        maxTimer = Random.Range(timerMin, timerMax);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    private void Update()
    {
        spawnY = Random.Range(-4, 4);
        //Debug.Log(spawnY);
        if (spawning == false && isReeling == false)
        {
            spawning = true;
            StartCoroutine(SpawnFishTimer());
        }
            //if the frog is reeling, stop spawning fish from bottom and start spawning them from top
        if (spawning == false && isReeling == true)
        {
            spawning = true;
            StopCoroutine(SpawnFishTimer());
            StartCoroutine(SpawnFishReelTimer());
        }
    }

    //Timer for how long an enemy will spawn
    IEnumerator SpawnFishTimer()
    {
        speed = 5f;
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.5f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(fishCollectible, new Vector2(spawnY, -18), fishCollectible.rotation);
        spawning = false;
    }

    IEnumerator SpawnFishReelTimer()
    {
        speed = -5f;
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.5f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(fishCollectible, new Vector2(spawnY, 0), fishCollectible.rotation);
        spawning = false;
    }
}
