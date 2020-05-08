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

        StartCoroutine("SpawnEnemyTimer");

    }

    private void Update()
    {
        spawnX = Random.Range(-4, 4);
        //Debug.Log(spawnX);
        if (spawningUp == "no")
        {
            spawningUp = "yes";
            StartCoroutine(SpawnEnemyTimer());
            isReeling = false;
        }

        //if reeling, stop spawning enemy from bottom and start spawning from top
        //if (reelPosition && isReeling == true)
        //{
        //    StopCoroutine(SpawnEnemyTimer());
        //    spawningDown = "yes";
        //    StartCoroutine(SpawnEnemyReelTimer());
        //}
    }

    //Timer for how long an enemy will spawn
    IEnumerator SpawnEnemyTimer()
    {
            maxTimer = Random.Range(timerMin, timerMax);
            timer += 0.2f;
            yield return new WaitForSeconds(maxTimer);
            Instantiate(jellyfishEnemy, new Vector2(spawnX, -18), jellyfishEnemy.rotation);
            spawningUp = "no";
    }

    IEnumerator SpawnEnemyReelTimer()
    {
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.2f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(jellyfishEnemy, new Vector2(spawnX, 0), jellyfishEnemy.rotation);
        spawningDown = "no";
    }
}



