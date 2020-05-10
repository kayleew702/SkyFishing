using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private float timer;
    private float maxTimer;
    private Vector2 screenBounds;

    public int spawnX = 0;
    public Transform jellyfishEnemy;
    public bool spawning = false;

    public bool isDiving;
    public bool isReeling;
    public bool reachedSurface;
    //public bool isReeling = GameObject.Find("frog").GetComponent<FrogController>().isReeling;
    //public float speed = GameObject.Find("Enemy").GetComponent<EnemyController>().speed;

    public GameObject frogPlayer;

    public float timerMin = 5f;
    public float timerMax = 12f;

    public GameObject divingSpawnPoint;
    public GameObject reelingSpawnPoint;

    private float divingSpawnX;
    private float reelingSpawnX;

    public bool spawningFromBottom;
    public bool spawningFromTop;

    public Coroutine divingCoroutine = null;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        maxTimer = Random.Range(timerMin, timerMax);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        divingSpawnX = divingSpawnPoint.transform.position.y;
        reelingSpawnX = reelingSpawnPoint.transform.position.y;
    }

    private void Update()
    {
        GetBools();

        spawnX = Random.Range(-4, 4);
        //Debug.Log(spawnX);
        if (spawning == false && isReeling == false)
        {
            spawning = true;
            divingCoroutine = StartCoroutine(SpawnEnemyTimer());
        }
        //if reeling, stop spawning enemy from bottom and start spawning from top
        else if (spawning == false && isReeling == true)
        {
            spawning = true;
            StopCoroutine(divingCoroutine);
            StartCoroutine(SpawnEnemyReelTimer());
        }
    }

    public void GetBools()
    {
        //get bools from Frog Controller script

        //if the frog is reeling, spawn enemies from the top
        isReeling = frogPlayer.GetComponent<FrogController>().isReeling;

        isDiving = frogPlayer.GetComponent<FrogController>().isDiving;

        //if the frog reached the surface, stop spawning enemies
        reachedSurface = frogPlayer.GetComponent<FrogController>().reachedSurface;
    }


    //Timer for how long an enemy will spawn
    IEnumerator SpawnEnemyTimer()
    {
        //speed = 4f;
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.2f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(jellyfishEnemy, new Vector2(spawnX, divingSpawnX), jellyfishEnemy.rotation);
        spawning = false;

        spawningFromBottom = true;
    }

    IEnumerator SpawnEnemyReelTimer()
    {
        //speed = -4f;
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.2f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(jellyfishEnemy, new Vector2(spawnX, 0), jellyfishEnemy.rotation);
        spawning = false;

        spawningFromTop = true;
    }
}



