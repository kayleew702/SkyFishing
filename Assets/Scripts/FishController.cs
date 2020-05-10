using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float downSpeed = -1.5f;
    public 
        float upSpeed = 1.5f;
    private Rigidbody2D rb;

    public bool isReeling;
    public bool changedSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, upSpeed);

        isReeling = GameObject.Find("frog").GetComponent<FrogController>().isReeling;

        changedSpeed = false;
    }


    // Update is called once per frame
    void Update()
    {
        //if bool isReeling in FishManager script is true, speed is negative
        isReeling = GameObject.Find("frog").GetComponent<FrogController>().isReeling;

        if (isReeling == true)
        {
            //set speed to negative
            rb.velocity = new Vector2(0, downSpeed);
            
        }

        if (transform.position.y > Camera.main.WorldToViewportPoint(transform.position).y)
        {
            Debug.Log("Fish destroyed");
            Destroy(this.gameObject);
        }
    }
}
