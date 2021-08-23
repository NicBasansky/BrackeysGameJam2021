using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    [Header("Interactable Info")]
    public float sphereCastRadius = 0.5f;
    public int interactableLayerIndex;
    private Vector3 raycastPos;
    public GameObject lookObject;
    private PickupObject pickupObject;
    private Camera mainCamera;

    [Header("Pickup")]
    [SerializeField] Transform pickupParent;
    public GameObject currentlyPickedUpObject;
    private Rigidbody pickupRB;

    [Header("ObjectFollow")]
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 300f;
    [SerializeField] private float maxDistance = 10f;
    private float currentSpeed = 0f;
    private float currentDist = 0f;

    [Header("Rotation")]
    public float rotationSpeed = 100f;
    Quaternion lookRotation;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // TODO see if there is a better way to do this, perhaps on clicking on something using the interactable
    void Update()
    {
        raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.SphereCast(raycastPos, sphereCastRadius, mainCamera.transform.forward, out hit, 
                                maxDistance, 1 << interactableLayerIndex))
        {
            lookObject = hit.collider.transform.root.gameObject;
        }
        else
        {
            lookObject = null;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // if we aren't holding anything
            if (currentlyPickedUpObject == null)
            {   
                // and we are looking at an interactable object
                if (lookObject != null)
                {
                    PickupObject();
                }
            }
            else
            {
                BreakConnection();
            }
        } // TODO press another button, maybe SPACE to launch an object

        if (currentlyPickedUpObject != null && currentDist > maxDistance)
            BreakConnection();
    }

    // velocity movement toward pickup parent and rotation
    private void FixedUpdate()
    {
        if (currentlyPickedUpObject != null)
        {
            currentDist = Vector3.Distance(pickupParent.position, pickupRB.position);
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDist / maxDistance);
            currentSpeed *= Time.fixedDeltaTime;
            // which direction do we want to apply the force
            Vector3 direction = pickupParent.position - pickupRB.position;
            pickupRB.velocity = direction.normalized * currentSpeed;
            // Rotation
            lookRotation = Quaternion.LookRotation(mainCamera.transform.position - currentlyPickedUpObject.transform.position);
            lookRotation = Quaternion.Slerp(mainCamera.transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
            pickupRB.MoveRotation(lookRotation);
        }
    }
    
    
    public void BreakConnection()
    {
        pickupRB.constraints = RigidbodyConstraints.None;
        currentlyPickedUpObject = null;
        pickupObject.pickedUp = false;
        currentDist = 0f;
    }

    private void PickupObject()
    {
        // GetComp in Children?
        pickupObject = lookObject.GetComponent<PickupObject>();
        currentlyPickedUpObject = lookObject;
        pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
        pickupRB.constraints = RigidbodyConstraints.FreezeRotation; // TODO necessary?
        pickupObject.objectHolder = this;
        StartCoroutine(pickupObject.PickUp());

    }




    // [SerializeField] Transform holdingTranform;
    // Transform originalParent = null;
    // Transform heldObject = null;

    // public void HoldObject(Transform obj) 
    // {
    //     originalParent = obj.parent;
    //     obj.parent = holdingTranform;
    //     heldObject = obj;
    // }

    // public void DropObject()
    // {
    //     heldObject.parent = originalParent;
    // }

}
