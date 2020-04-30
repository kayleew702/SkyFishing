using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public float speed;
    private float moveY;
    private float moveX;

    private Rigidbody2D rb;

    public int FishCollected = 0;
    public int totalFish = 0;

    public Transform startPos;

    Animator animator;

    private bool IsReeling;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BoundMovement();
        
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveX = x * speed;
        moveY = y * speed;

        rb.velocity = new Vector2(moveX, moveY);
    }

    //keep frog from going out of water
    void BoundMovement()
    {
        float dist = (this.transform.position - Camera.main.transform.position).z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        //float topBorder =
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, -11.72f, dist)).y;

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
            if (IsReeling == true)
            {
                FishCollected = 0;

            }
            else if (IsReeling == false)
            {
                Reel();
            }
        }

        //what to do if frog hits fish
        if (collision.gameObject.tag == "Fish")
        {
            FishCollected += 1;
            Reel();
        }
    }

    void Reel()
    {
        IsReeling = true;
        //move back to start position


    }
}
