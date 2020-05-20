using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMovement : MonoBehaviour
{
    public bool pauseMenu;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("Pause Menu Controller").GetComponent<PauseMenuController>().menuActivated;
    }

    // Update is called once per frame
    void Update()
    {
        pauseMenu = GameObject.Find("Pause Menu Controller").GetComponent<PauseMenuController>().menuActivated;

        if (pauseMenu == true && paused == false)
        {
            GameObject.Find("frog").GetComponent<FrogController>().enabled = false;
        }

        if (pauseMenu == false)
        {
            GameObject.Find("frog").GetComponent<FrogController>().enabled = true;
        }
        
    }
}
