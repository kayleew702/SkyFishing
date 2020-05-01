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


    public Transform startPos;

    Animator animator;

    public bool isReeling = false;
    public bool isDiving = false;

    //Gives access to fish collected text in UI
    private Text fishCollectedText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Gives access to fish collected text in UI
        fishCollectedText = GameObject.Find("FishCollectedText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BoundMovement();

        //frog begins diving when the CameraMovement code says so
        Dive();

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
            //resets fishCollected variable to 0 inside the FishCollectedController script, which is attached to the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().fishCollected = 0;
            //runs the UpdateFishCollected method, which updates the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().UpdateFishCollected();

            if (isReeling == true)
            {


            }

            else if (isReeling == false)
            {


                Reel();
            }
        }

        //what to do if frog hits fish
        if (collision.gameObject.tag == "Fish")
        {
            //adds 1 to the fishCollected variable inside the FishCollectedController script, which is attached to the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().fishCollected += 1;
            //runs the UpdateFishCollected method, which updates the fishCollectedText game object
            fishCollectedText.GetComponent<FishCollectedController>().UpdateFishCollected();

            if (isReeling == true)
            {

            }

            else if (isReeling == false)
            {
                Reel();
            }

        }
    }

    public void Reel()
    {
        isReeling = true;
        //move back to start position


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
