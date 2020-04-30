using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    //private Vector2 screenBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed);
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }


    // Update is called once per frame
    void Update()
    {
        //if (Camera.main.WorldToViewportPoint(transform.position).y > 0)
        //    Debug.Log("Enemy Destroyed");
        //    Destroy(this.gameObject);
        //if (transform.position.y < screenBounds.y * 2)
        //    Debug.Log("Enemy Destroyed");
        //    Destroy(this.gameObject);
        if (transform.position.y > Camera.main.WorldToViewportPoint(transform.position).y)
        {
            Debug.Log("Enemy destroyed");
            Destroy(this.gameObject);
        }
    }
}
