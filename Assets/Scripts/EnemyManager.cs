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
    public bool isReeling = GameObject.Find("frog").GetComponent<FrogController>().isReeling;
    public float speed = GameObject.Find("Enemy").GetComponent<EnemyController>().speed;

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
        spawnX = Random.Range(-4, 4);
        //Debug.Log(spawnX);
        if (spawning == false && isReeling == false)
        {
            spawning = true;
            StartCoroutine(SpawnEnemyTimer());
        }
        //if reeling, stop spawning enemy from bottom and start spawning from top
        if (spawning == false && isReeling == true)
        {
            spawning = true;
            StopCoroutine(SpawnEnemyTimer());
            StartCoroutine(SpawnEnemyReelTimer());
        }
    }

    //Timer for how long an enemy will spawn
    IEnumerator SpawnEnemyTimer()
    {
        speed = 4f;
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.2f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(jellyfishEnemy, new Vector2(spawnX, -18), jellyfishEnemy.rotation);
        spawning = false;
    }

    IEnumerator SpawnEnemyReelTimer()
    {
        speed = -4f;
        maxTimer = Random.Range(timerMin, timerMax);
        timer += 0.2f;
        yield return new WaitForSeconds(maxTimer);
        Instantiate(jellyfishEnemy, new Vector2(spawnX, 0), jellyfishEnemy.rotation);
        spawning = false;
    }
}



