using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteOnPause : MonoBehaviour
{
    public bool pauseMenu;
    public bool muted;
    public AudioClip reelSound;
    public AudioSource diveSound;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("Pause Menu Controller").GetComponent<PauseMenuController>().menuActivated;
        diveSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        pauseMenu = GameObject.Find("Pause Menu Controller").GetComponent<PauseMenuController>().menuActivated;

        //when the pauseMenu is activated, mute the frog sounds
        if (pauseMenu == true && muted == false)
        {
            diveSound.mute = true;
            muted = true;
        }

        //unmute when pause menu is deactivated
        if (pauseMenu == false && muted == true)
        {
            diveSound.mute = false;
            muted = false;
        }
        
    }
}
