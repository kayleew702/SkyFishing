using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //camera only moves in y direction
    //have a beginning position and ending position as empty game objects (at the top of the camera)

    public Transform currentPositionTransform;
    private float smoothSpeed = 0.01f;

    public float cameraIncrement = .01f;
    public bool reachedEnd = false;

    public Vector3 currentPosition;

    public float newYPos;

    public GameObject cameraStart;
    public GameObject cameraEnd;

    private float cameraHalfHeight;



    //currentPosition will start with the initial position

    void Start()
    {
        //find half the width of the camera's aspect ratio
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    void FixedUpdate()
    {
        //every time there's an update, we find the new current top and bottom borders of the camera
        float borderTop = cameraStart.transform.position.y + cameraHalfHeight;
        float borderBottom = cameraEnd.transform.position.y + cameraHalfHeight;
        float borderBottomBuffer = borderBottom - 1;

        if (reachedEnd == false)
        {
            newYPos -= cameraIncrement;

            currentPosition = Vector3.Lerp(this.transform.position,
                new Vector3(
                    this.transform.position.x,
                    Mathf.Clamp(newYPos, borderBottomBuffer, this.transform.position.y),
                    this.transform.position.z),
                smoothSpeed);

            this.transform.position = currentPosition;
        }


        //if the camera reaches the bottom border, stop moving
        if (this.transform.position.y <= borderBottom)
        {
            Debug.Log("Reached Bottom");
            reachedEnd = true;
        }
    }
}
