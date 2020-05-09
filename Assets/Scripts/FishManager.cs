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
    public bool isReeling;
    public bool reachedSurface;
    public float speed = GameObject.Find("Fish").GetComponent<FishController>().speed;

    public GameObject frogPlayer;

    public float timerMin = 5f;
    public float timerMax = 12f;

    public GameObject divingSpawnPoint;
    public GameObject reelingSpawnPoint;

    private float divingSpawnY;
    private float reelingSpawnY;

    public bool spawningFromTop;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        maxTimer = Random.Range(timerMin, timerMax);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        divingSpawnY = divingSpawnPoint.transform.position.y;
        reelingSpawnY = reelingSpawnPoint.transform.position.y;
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
        else if (spawning == false && isReeling == true)
        {
            spawning = true;
            StopCoroutine(SpawnFishTimer());
            StartCoroutine(SpawnFishReelTimer());
        }

        //if the frog reached the surface, stop spawning fish
        //if (reachedSurface == true)
        {
            //StopCoroutine(SpawnFishReelTimer());
        }
        

        GetBools();
    }

    public void GetBools()
    {
        //gets bools from the Frog Controller script

        //if the frog is reeling, spawn fish from the top
        isReeling = frogPlayer.GetComponent<FrogController>().isReeling;

        //if the frog reached the surface, stop spawning fish
        reachedSurface = frogPlayer.GetComponent<FrogController>().reachedSurface;
    }

    //Timer for how long an enemy will spawn
    IEnumerator SpawnFishTimer()
    {
        

        if (isReeling == false)
        {
            speed = 5f;
            //GameObject.Find("Fish").GetComponent<FishController>().speed = 5f;
            maxTimer = Random.Range(timerMin, timerMax);
            timer += 0.5f;
            yield return new WaitForSeconds(maxTimer);
            Instantiate(fishCollectible, new Vector2(spawnY, divingSpawnY), fishCollectible.rotation);
            spawning = false;
        }

        else
        {
            
        }

    }

    IEnumerator SpawnFishReelTimer()
    {
        //speed = -5f;
        //maxTimer = Random.Range(timerMin, timerMax);
        //timer += 0.5f;
        //yield return new WaitForSeconds(maxTimer);
        //Instantiate(fishCollectible, new Vector2(spawnY, reelingSpawnY), fishCollectible.rotation);
        //spawning = false;

        speed = -5f;
        //GameObject.Find("Fish").GetComponent<FishController>().speed = -5f;
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.5f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(fishCollectible, new Vector2(spawnY, reelingSpawnY), fishCollectible.rotation);
        spawning = false;
        //spawningFromTop bool is used to check if the SpawnFishReelTimer() is running, which it isnt for some reason :(
        spawningFromTop = true;
    }
}
