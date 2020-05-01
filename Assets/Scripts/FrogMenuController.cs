using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMenuController : MonoBehaviour
{
    public float speed;
    private float moveY;
    private float moveX;

    public GameObject leftBoundary;
    public GameObject rightBoundary;

    private Rigidbody2D rb;

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
        //float y = Input.GetAxisRaw("Vertical");

        moveX = x * speed;
        //moveY = y * speed;

        rb.velocity = new Vector2(moveX, 0);
    }

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
}
