using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FrogController : MonoBehaviour
{
    public bool pauseMenu;
    public GameObject pauseMenuObject;

    public float speed = 5;
    //private float moveY;
    public float moveX;

    private Rigidbody2D rb;

    public int FishCollected = 0;
    public int totalFish = 0;

    private float smoothSpeed = 0.01f;

    public Vector3 currentPosition;
    private Vector3 frogSize;

    public GameObject startPosition;
    public GameObject divingPosition;
    public GameObject reelingPosition;
    public GameObject leftBoundary;
    public GameObject rightBoundary;

    private GameObject endScore;

    public float newYPos;
    public float yPosIncrement = .03f;

    public Animator animator;

    public AudioSource diveSound;
    public AudioSource reelSound;
    public AudioSource enemyHit;
    public AudioSource collectFish;

    public bool reachedReelPos;
    public bool isReeling;
    public bool isDiving;
    public bool reachedSurface;


    public int currentDepth;

    //Gives access to fish collected text in UI
    private Text fishCollectedText;
    private Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Gives access to fish collected text in UI
        fishCollectedText = GameObject.Find("FishCollectedText").GetComponent<Text>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        endScore = GameObject.Find("BigScore");
        endScore.SetActive(false);

        isDiving = true;
        animator.SetBool("IsDive", true);
        animator.SetBool("IsReel", false);
        diveSound.Play();

        pauseMenu = pauseMenuObject.GetComponent<PauseMenuController>().menuActivated;
    }

    // Update is called once per frame
    void Update()
    {
        pauseMenu = pauseMenuObject.GetComponent<PauseMenuController>().menuActivated;

        Move();
        BoundMovement();

        //frog begins diving at the beginning of the level
        Dive();

        Reel();

        //after reeling and the depth meter is 0, the frog will return to the start position
        StartPos();

        if (isReeling == true)
        {
            animator.SetBool("IsReel", true);
            animator.SetBool("IsDive", false);

            if (reelSound.isPlaying == false)
            {
                reelSound.Play();
            }
            
            
        }
        else
        {
            animator.SetBool("IsDive", true);
            animator.SetBool("IsReel", false);

        }



    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        moveX = x * speed;
        //moveY = y * speed;

        if (moveX > 0)
        {
            animator.SetBool("SwimRight", true);
            animator.SetBool("SwimLeft", false);
        }
        else if (moveX < 0)
        {
            animator.SetBool("SwimLeft", true);
            animator.SetBool("SwimRight", false);
        }
        else
        {
            animator.SetBool("SwimLeft", false);
            animator.SetBool("SwimRight", false);
        }

        if (animator.GetBool("HitEnemy"))
        {
            animator.SetBool("SwimLeft", false);
            animator.SetBool("SwimRight", false);
        }

        if (animator.GetBool("HitFish"))
        {
            animator.SetBool("SwimLeft", false);
            animator.SetBool("SwimRight", false);
        }
        

        rb.velocity = new Vector2(moveX, 0);
    }

    //keep frog from going out of water
    void BoundMovement()
    {
        float dist = (this.transform.position - Camera.main.transform.position).z;

        //float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        //float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, -11.72f, dist)).y;

        float leftBorder = leftBoundary.transform.position.x;
        float rightBorder = rightBoundary.transform.position.x;

        if (isDiving == true)
        {
            frogSize = this.GetComponent<frogSpriteController>().frogDiveSprite.bounds.size;
        }
        else if (isReeling == true)
        {
            frogSize = this.GetComponent<frogSpriteController>().frogReelSprite.bounds.size;
        }

        this.transform.position = new Vector3(Mathf.Clamp
            (this.transform.position.x, leftBorder + frogSize.x / 2, rightBorder - 1 - frogSize.x / 2),
            this.transform.position.y, this.transform.position.z);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //What to do when frog hits enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (animator.GetBool("HitFish"))
            {
                animator.SetBool("HitEnemy", false);
            }
            else
            {
                animator.SetBool("HitEnemy", true);
            }

            enemyHit.Play();
            //resets fishCollected variable to 0 inside the FishCollectedController script, which is attached to the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().fishCollected = 0;

            //runs the UpdateFishCollected method, which updates the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().UpdateFishCollected();
            //highScoreText.GetComponent<HighScoreController>().UpdateHighScore();
            FishCollected = 0;
            GameObject.Destroy(collision.gameObject);
            Invoke("ResetBools", 2f);

            if (isReeling == false)
            {
                isReeling = true;
                Reel();
            }
        }

        //what to do if frog hits fish
        if (collision.gameObject.CompareTag("Fish"))
        {

            if (animator.GetBool("HitEnemy"))
            {
                animator.SetBool("HitFish", false);
            }
            else
            {
                animator.SetBool("HitFish", true);
            }

            collectFish.Play();
            FishCollected += 1;
            //adds 1 to the fishCollected variable inside the FishCollectedController script, which is attached to the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().fishCollected += 1;

            //runs the UpdateFishCollected method, which updates the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().UpdateFishCollected();
            GameObject.Destroy(collision.gameObject);
            Invoke("ResetBools", 1f);

            if (isReeling == false)
            {
                isReeling = true;
                Reel();
            }

        }
    }

    public void ResetBools()
    {
        animator.SetBool("HitFish", false);
        animator.SetBool("HitEnemy", false);
    }

    public void ReelPosition()
    {
        

        //move to reel position

        //only activate if the menu isn't open


        float reelingPositionY = reelingPosition.transform.position.y;
        float reelingPositionBuffer = reelingPositionY - 1;

        if ((reachedReelPos == false) && (isReeling == true) && (pauseMenu == false))
        {

            newYPos -= yPosIncrement;

            if (newYPos < this.transform.position.y)
            {
                newYPos = this.transform.position.y;
            }
            

            currentPosition = Vector3.Lerp(this.transform.position,
            new Vector3(
                this.transform.position.x,
                Mathf.Clamp(newYPos, this.transform.position.y, reelingPositionBuffer),
                this.transform.position.z),
            smoothSpeed);

            //currentPosition = new Vector3(this.transform.position.x, newYPos, this.transform.position.z);

            this.transform.position = currentPosition;
        }

        //when the frog reaches the diving position, stop moving down
        if (this.transform.position.y <= reelingPositionY)
        {
            Debug.Log("Reached reel position");
            reachedReelPos = true;
        }
    }

    public void Reel()
    {
        
        ReelPosition();

        currentDepth = GameObject.Find("DepthMeter").GetComponent<DepthController>().currentDepth;

        if (currentDepth == 0)
        {
            isReeling = false;
            reachedSurface = true;
            //highScoreText.GetComponent<HighScoreController>().UpdateHighScore();
            Invoke("EndScore", 2f);
            Invoke("LoadMenu", 5f);
        }

        //when the depth meter reaches 0, frogIsReeling = false
        //and the score is displayed
    }

    public void EndScore()
    {
        endScore.SetActive(true);
        endScore.GetComponent<BigScoreScript>().ShowScore();
    }

    public void LoadMenu()
    {
        if (isReeling == false && reachedSurface == true && currentDepth == 1)
        {
            SceneManager.LoadScene("Menu");
            Debug.Log("Startup Menu Loaded");
        }
    }

    public void StartPos()
    {
        //when the depth meter is 0, the frog goes to the top of the water (back to its starting point)
        float startPositionY = startPosition.transform.position.y;
        float startPositionBuffer = startPositionY + 1;

        if (reachedSurface == true && pauseMenu == false)
        {
            currentPosition = Vector3.Lerp(this.transform.position,
                new Vector3(
                    this.transform.position.x,
                    Mathf.Clamp(newYPos, startPositionBuffer, this.transform.position.y),
                    this.transform.position.z),
                smoothSpeed);

            this.transform.position = currentPosition;
        }
    }

    public void Dive()
    {
        isDiving = true;

        float divingPositionY = divingPosition.transform.position.y;
        float divingPositionBuffer = divingPositionY - 2;

        if (isDiving == true)
        {

            newYPos -= yPosIncrement;

            currentPosition = Vector3.Lerp(this.transform.position,
                new Vector3(
                    this.transform.position.x,
                    Mathf.Clamp(newYPos, this.transform.position.y, divingPositionBuffer),
                    this.transform.position.z),
                smoothSpeed);

            this.transform.position = currentPosition;
        }

        //when the frog reaches the diving position, stop moving down
        if (this.transform.position.y <= divingPositionY)
        {
            Debug.Log("No longer diving");
            isDiving = false;
        }

    }

}
