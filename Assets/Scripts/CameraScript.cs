using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class CameraScript : MonoBehaviour
{
    Vector3 difference;
    Vector3 origin;
    Camera cam;
    bool drag = false;
    public bool firstClickedNothing = false;
    public static CameraScript mainCamera;

    void Start()
    {
        cam = GetComponent<Camera>();

        // Calculate the desired aspect ratio (e.g., 16:9)
        float targetAspect = 16f / 9f;
        // Calculate the current aspect ratio
        float currentAspect = (float)Screen.width / Screen.height;

        // Calculate the viewport width and height based on the current aspect ratio and the desired aspect ratio
        float viewportWidth = 1f;
        float viewportHeight = 1f;

        if (currentAspect < targetAspect)
        {
            // Add black bars on the sides
            viewportWidth = currentAspect / targetAspect;
        }
        else
        {
            // Add black bars on the top and bottom
            viewportHeight = targetAspect / currentAspect;
        }

        // Set the viewport rect of the Camera to add black bars if necessary
        Camera.main.rect = new Rect((1f - viewportWidth) / 2f, (1f - viewportHeight) / 2f, viewportWidth, viewportHeight);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(cam.GetComponent<PixelPerfectCamera>().pixelRatio);// += Input.mouseScrollDelta.y;



        if (Input.GetMouseButtonDown(0))
        {
            firstClickedNothing = true;
        }


        if (Input.GetMouseButton(0) && firstClickedNothing)
        {
            difference = cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position;
            if (drag == false)
            {
                drag = true;
                origin = cam.ScreenToWorldPoint(Input.mousePosition);
            }

        }
        else
        {
            drag = false;
        }
/*
        float roundedX = Mathf.Round(difference.x / 0.0625f) * 0.0625f;
        float roundedY = Mathf.Round(difference.y / 0.0625f) * 0.0625f;
        float roundedZ = Mathf.Round(difference.z / 0.0625f) * 0.0625f;
        difference = new Vector3(roundedX, roundedY, roundedZ);*/

        if (drag) transform.position = origin - difference;

        Vector3 position = Camera.main.transform.position;
        float roundedX = Mathf.Round(position.x / 0.0625f) * 0.0625f;
        float roundedY = Mathf.Round(position.y / 0.0625f) * 0.0625f;
        float roundedZ = Mathf.Round(position.z / 0.0625f) * 0.0625f;

        Vector3 roundedPosition = new Vector3(roundedX, roundedY, roundedZ);
        Camera.main.transform.position = roundedPosition;
    }
}