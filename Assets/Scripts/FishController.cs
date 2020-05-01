using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed);
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Camera.main.WorldToViewportPoint(transform.position).y)
        {
            Debug.Log("Fish destroyed");
            Destroy(this.gameObject);
        }
    }
}
