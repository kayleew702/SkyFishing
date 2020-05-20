using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjects : MonoBehaviour
{
    private bool reachedSurface;

    // Start is called before the first frame update
    void Start()
    {
        reachedSurface = false;
    }

    // Update is called once per frame
    void Update()
    {
        reachedSurface = GameObject.Find("frog").GetComponent<FrogController>().reachedSurface;

        if (reachedSurface == true)
        {
            Destroy(this.gameObject);
        }

    }
}
