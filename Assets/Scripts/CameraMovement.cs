using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //camera only moves in y direction
    //have a beginning position and ending position as empty game objects (at the top of the camera)

    private float smoothSpeed = 0.01f;
    public float cameraIncrement = .01f;
    public float reelingCamInc = 10;

    public bool movingDown = true;

    public Vector3 currentPosition;
    public float newYPos;
    private float cameraHalfHeight;

    public GameObject frogPlayer;
    public GameObject reelBoundaryObj;
    public GameObject diveBoundaryObj;
    public GameObject surfaceBoundaryObj;

    public float startingPoint;
    public float diveBoundary;

    public bool frogIsReeling;
    public bool reachedSurface;


    //currentPosition will start with the initial position

    void Start()
    {
        startingPoint = Camera.main.transform.position.y;

        movingDown = true;

        //find half the height of the camera's aspect ratio
        cameraHalfHeight = Camera.main.orthographicSize;

        FrogBools();
    }

    void FixedUpdate()
    {
        //borderTop and borderBottom variables will update
        //camera will move down if reachedEnd == false
        MoveDown();

        ReelingPos();

        ReachSurface();
    }

    void Update()
    {
        FrogBools();
    }

    public void FrogBools()
    {
        //gets bools from the Frog Controller script
        frogIsReeling = frogPlayer.GetComponent<FrogController>().isReeling;
        reachedSurface = frogPlayer.GetComponent<FrogController>().reachedSurface;
    }

    public void MoveDown()
    {
        //method in which the camera moves down so that the frog is at the top of the screen

        //final position
        diveBoundary = diveBoundaryObj.transform.position.y - cameraHalfHeight + 2;
        float diveBoundaryBuffer = diveBoundary - 1;

        if (movingDown == true)
        {
            //both the camera and frogPosSnap move down by the cameraIncrement
            cameraIncrement = reelingCamInc;
            newYPos -= cameraIncrement;

            currentPosition = Vector3.Lerp(this.transform.position,
                new Vector3(
                    this.transform.position.x,
                    Mathf.Clamp(newYPos, diveBoundaryBuffer, this.transform.position.y),
                    this.transform.position.z),
                smoothSpeed);

            this.transform.position = currentPosition;
        }

        if (this.transform.position.y <= diveBoundary)
        {
            movingDown = false;
        }

    }

    public void ReelingPos()
    {
        float reelBoundary = reelBoundaryObj.transform.position.y + cameraHalfHeight - 2;
        float reelBoundaryBuffer = reelBoundary + 1;

        bool initialYPos = false;
        if (initialYPos == false)
        {
            newYPos = this.transform.position.y;
            initialYPos = true;
        }
        
        if (frogIsReeling == true)
        {
            cameraIncrement = reelingCamInc;
            //both the camera and frogPosSnap move down by the cameraIncrement
            newYPos -= cameraIncrement;

            currentPosition = Vector3.Lerp(this.transform.position,
                new Vector3(
                    this.transform.position.x,
                    Mathf.Clamp(newYPos, reelBoundaryBuffer, this.transform.position.y),
                    this.transform.position.z),
                smoothSpeed);

            this.transform.position = currentPosition;
        }
    }

    public void ReachSurface()
    {
        
        float startingPoint = surfaceBoundaryObj.transform.position.y;
        float startingPointBuffer = startingPoint + 1;

        if (reachedSurface == true)
        {

            newYPos += cameraIncrement;

            currentPosition = Vector3.Lerp(this.transform.position,
                new Vector3(
                    this.transform.position.x,
                    Mathf.Clamp(newYPos, this.transform.position.y, startingPointBuffer),
                    this.transform.position.z),
                smoothSpeed);

            //currentPosition = new Vector3(this.transform.position.x, newYPos, this.transform.position.z);

            this.transform.position = currentPosition;
        }

        if (this.transform.position.y >= startingPoint)
        {
            reachedSurface = false;
        }

    }
}
