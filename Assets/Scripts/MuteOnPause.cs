using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteOnPause : MonoBehaviour
{
    public bool pauseMenu;
    public bool muted;
    public AudioSource reelSound;
    public AudioSource diveSound;
    public AudioSource backgroundMusicSound;
    public AudioSource[] audioSourceList;
    public List<AudioSource> soundsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("Pause Menu Controller").GetComponent<PauseMenuController>().menuActivated;
        diveSound = GetComponent<AudioSource>();

        backgroundMusicSound = GameObject.Find("Background Music").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        pauseMenu = GameObject.Find("Pause Menu Controller").GetComponent<PauseMenuController>().menuActivated;

        audioSourceList = FindObjectsOfType<AudioSource>();
        foreach (AudioSource sound in audioSourceList)
        {
            if (sound.isPlaying == true && (sound != backgroundMusicSound))
            {
                soundsPlaying.Add(sound);
            }
        }

        //when the pauseMenu is activated, mute all sounds except background sound
        if (pauseMenu == true && muted == false)
        {
            foreach (AudioSource soundPlaying in soundsPlaying)
            {
                soundPlaying.mute = true;
                muted = true;
            }
        }

        //unmute when pause menu is deactivated
        if (pauseMenu == false && muted == true)
        {
            foreach (AudioSource soundPaused in soundsPlaying)
            {
                soundPaused.mute = false;
                muted = false;
            }
        }
        
    }
}
