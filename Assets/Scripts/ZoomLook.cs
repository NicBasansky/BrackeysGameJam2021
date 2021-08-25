using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomLook : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float zoomInFov = 37f;

    [SerializeField] float lineTraceRange = 3f;

    float initialFOV;

    void Start()
    {
        initialFOV = cam.fieldOfView;
    }


    public void ZoomIn() // todo smooth camera
    {
        cam.fieldOfView = zoomInFov;
    }

    public void ZoomOut()
    {
        cam.fieldOfView = initialFOV;

    }

   
}
