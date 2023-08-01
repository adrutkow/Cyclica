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

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        mainCamera = this;
    }

    // Update is called once per frame
    void Update()
    {

        cam.orthographicSize -= Input.mouseScrollDelta.y;

        if (cam.orthographicSize < 1) cam.orthographicSize = 1;
        if (cam.orthographicSize > 14) cam.orthographicSize = 14;


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

        if (drag) transform.position = origin - difference;
    }
}