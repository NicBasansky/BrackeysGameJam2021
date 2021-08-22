using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomLook : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float zoomInFov = 37f;

    [SerializeField] float lineTraceRange = 3f;

    float initialFOV;

    void Start()
    {
        initialFOV = camera.fieldOfView;
    }

    void Update()
    {
        if(Input.GetButtonDown("Zoom") || Input.GetMouseButtonDown(1))
        {
            ZoomIn();          
            ProcessRaycast();
        }
        else if (Input.GetButtonUp("Zoom") || Input.GetMouseButtonUp(1))
        {
            ZoomOut();
        }
    }

    void ZoomIn() // todo smooth camera
    {
        camera.fieldOfView = zoomInFov;
    }

    void ProcessRaycast()
    {   
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, lineTraceRange))
        {
            var interactableObj = hit.collider.GetComponent<IInteractable>();
            if(interactableObj != null)
            {
                interactableObj.OnInteract();
            }          
        }
        Debug.DrawLine(camera.transform.position, transform.position + (camera.transform.forward * lineTraceRange), Color.red, 5f);
    }

    void ZoomOut()
    {
        camera.fieldOfView = initialFOV;

    }
   
}
