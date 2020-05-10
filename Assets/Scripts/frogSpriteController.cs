using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogSpriteController : MonoBehaviour
{
    public SpriteRenderer frogDiveSprite;
    public SpriteRenderer frogReelSprite;

    public bool isDiving;
    public bool isReeling;

    void Start()
    {
        isDiving = this.GetComponent<FrogController>().isDiving;
        isReeling = this.GetComponent<FrogController>().isReeling;
    }

    void Update()
    {
        isDiving = this.GetComponent<FrogController>().isDiving;
        isReeling = this.GetComponent<FrogController>().isReeling;

        if (isDiving == true)
        {
            //enable frog dive sprite
            FrogDiveSprite();
        }

        if (isReeling == true)
        {
            //enable reel sprite
            FrogReelSprite();
        }
    }

    void FrogDiveSprite()
    {
        frogDiveSprite.enabled = true;
        frogReelSprite.enabled = false;
    }

    void FrogReelSprite()
    {
        frogReelSprite.enabled = true;
        frogDiveSprite.enabled = false;
    }
}
