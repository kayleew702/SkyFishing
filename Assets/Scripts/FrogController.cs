using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogController : MonoBehaviour
{
    public float speed;
    private float moveY;
    private float moveX;

    private Rigidbody2D rb;

    public int FishCollected = 0;
    public int totalFish = 0;

    private float smoothSpeed = 0.01f;

    public Vector3 currentPosition;

    public GameObject startPosition;
    public GameObject divingPosition;
    public GameObject reelingPosition;
    public GameObject leftBoundary;
    public GameObject rightBoundary;

    public float newYPos;
    public float yPosIncrement = .01f;

    Animator animator;

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

        isDiving = true;
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        BoundMovement();

        //frog begins diving at the beginning of the level
        Dive();

        //frog is reeled in when it collides with an enemy or a fish
        Reel();

        //after reeling and the depth meter is 0, the frog will return to the start position
        StartPos();


    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        moveX = x * speed;
        //moveY = y * speed;

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

        Vector3 frogSize = GetComponent<Renderer>().bounds.size;

        this.transform.position = new Vector3(Mathf.Clamp
            (this.transform.position.x, leftBorder + frogSize.x / 2, rightBorder - 1 - frogSize.x / 2),
            this.transform.position.y, this.transform.position.z);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //What to do when frog hits enemy
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject.Destroy(collision.gameObject);
            //resets fishCollected variable to 0 inside the FishCollectedController script, which is attached to the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().fishCollected = 0;
            FishCollected = 0;
            //runs the UpdateFishCollected method, which updates the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().UpdateFishCollected();
            highScoreText.GetComponent<HighScoreController>().UpdateHighScore();

            if (isReeling == false)
            {
                isReeling = true;
                Reel();
            }
        }

        //what to do if frog hits fish
        if (collision.gameObject.tag == "Fish")
        {
            FishCollected += 1;
            GameObject.Destroy(collision.gameObject);
            //adds 1 to the fishCollected variable inside the FishCollectedController script, which is attached to the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().fishCollected += 1;

            //runs the UpdateFishCollected method, which updates the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().UpdateFishCollected();
            GameObject.Destroy(collision.gameObject);

            if (isReeling == false)
            {
                isReeling = true;
                Reel();
            }

        }
    }

    public void ReelPosition()
    {
        //move to reel position

        float reelingPositionY = reelingPosition.transform.position.y;
        float reelingPositionBuffer = reelingPositionY - 1;

        if ((reachedReelPos == false) && (isReeling == true))
        {
            if (newYPos < this.transform.position.y)
            {
                newYPos = this.transform.position.y;
            }
            newYPos -= yPosIncrement;

            //currentPosition = Vector3.Lerp(this.transform.position,
            //new Vector3(
            //this.transform.position.x,
            //Mathf.Clamp(newYPos, this.transform.position.y, reelingPositionBuffer),
            // this.transform.position.z),
            //  smoothSpeed);

            currentPosition = new Vector3(this.transform.position.x, newYPos, this.transform.position.z);

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
        }

        //when the depth meter reaches 0, frogIsReeling = false
        //and the score is displayed
    }

    public void StartPos()
    {
        //when the depth meter is 0, the frog goes to the top of the water (back to its starting point)
        float startPositionY = startPosition.transform.position.y;
        float startPositionBuffer = startPositionY + 1;

        if (reachedSurface == true)
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
        float divingPositionBuffer = divingPositionY - 1;

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
