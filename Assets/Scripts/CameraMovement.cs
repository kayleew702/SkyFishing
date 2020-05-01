using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //camera only moves in y direction
    //have a beginning position and ending position as empty game objects (at the top of the camera)

    private float smoothSpeed = 0.01f;
    public float cameraIncrement = .01f;

    public bool movingDown = true;
    public bool reel = false;

    //this will come from the frog controller code
    public bool isDiving;
    public GameObject frogPlayer;

    public Vector3 currentPosition;
    public float newYPos;

    public GameObject diveBoundaryObj;
    public float diveBoundary;

    private float cameraHalfHeight;



    //currentPosition will start with the initial position

    void Start()
    {
        movingDown = true;

        //find half the height of the camera's aspect ratio
        cameraHalfHeight = Camera.main.orthographicSize;

        //gets bool isDiving from the Frog Controller script
        frogPlayer = GameObject.Find("Frog");
        isDiving = frogPlayer.GetComponent<FrogController>().isDiving;
    }

    void FixedUpdate()
    {

        //borderTop and borderBottom variables will update
        //camera will move down if reachedEnd == false
        MoveDown();

        //camera will move up if reel == true
        MoveUp();

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
            newYPos -= cameraIncrement;

            currentPosition = Vector3.Lerp(this.transform.position,
                new Vector3(
                    this.transform.position.x,
                    Mathf.Clamp(newYPos, diveBoundaryBuffer, this.transform.position.y),
                    this.transform.position.z),
                smoothSpeed);

            this.transform.position = currentPosition;


        }




        //when isDiving == false in the frog controlle code, the MoveDown() function will end
        if (isDiving == false)
        {
            Debug.Log("Reached Bottom");
            //movingDown = false;
        }



    }

    public void MoveUp()
    {

    }
}
