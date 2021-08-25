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

    [Header("Throwing Object")]
    [SerializeField] float maxThrowingForce = 30f; // KN (kg ms^-2)
    [Range(0, 1.0f)]
    [SerializeField] float throwingPercent = 1.0f; // TODO set to 0, then increase as holding spacebar
    //private Vector3 throwingUnitVector; // force direction, TODO need to declare here?
    private float currentThrust; // N
    private Vector3 throwingVector;
    private bool isThrowing = false;
    [SerializeField] float maxChargingSeconds = 3.5f;
    private float extraThrowingPercentThisFrame = 0f;
    private bool windUp = false;
    private float windUpSpeed;
    [SerializeField] Vector3 windUpPos = new Vector3(0, 0.35f, 1.5f);

    [Header("Pickup")]
    [SerializeField] Transform pickupParent;
    public GameObject currentlyPickedUpObject;
    private Rigidbody pickupRB;

    [Header("Object Follow")]
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
        extraThrowingPercentThisFrame = (1 * Time.deltaTime) / maxChargingSeconds;
        windUpSpeed = 1 / maxChargingSeconds;
    }

    // TODO see if there is a better way to do this, perhaps on clicking on something using the interactable
    void Update()
    {
        raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.SphereCast(raycastPos, sphereCastRadius, mainCamera.transform.forward, out hit, 
                                maxDistance, 1 << interactableLayerIndex))
        {
            lookObject = hit.collider.transform.gameObject;
        }
        else
        {
            lookObject = null;
        }

        // Inputs to pick up object
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
        } 

        // Inputs to throw object
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentlyPickedUpObject != null)
            {
                // build up the throwing percent of max throwing force, increase force bar
                //throwingPercent += (maxChargingSeconds /)
                InvokeRepeating("IncreaseThrowingPercent", 0.5f, Time.deltaTime);
                windUp = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
                // Throw object, add force in the camera's forward direction
            // add force
            if (currentlyPickedUpObject != null)
            {
                CancelInvoke();
                ExertForce();
                BreakConnection();
            }
            throwingPercent = 0f;
            windUp = false;
        }

        if (currentlyPickedUpObject != null && currentDist > maxDistance)
            BreakConnection();

        if (windUp)
        {
            pickupRB.transform.position = Vector3.Lerp(pickupRB.transform.position, mainCamera.transform.position +
                                                    windUpPos, windUpSpeed * Time.deltaTime);
        }
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

        if (isThrowing)
        {
            pickupRB.AddForce(throwingVector);
            isThrowing = false;
        }
    }
    
    private void IncreaseThrowingPercent()
    {
        if (throwingPercent < 1)
        {
            throwingPercent += extraThrowingPercentThisFrame;
        }
    }

    private void ExertForce()
    {
        currentThrust = throwingPercent * maxThrowingForce * 1000; // to convert into KiloNewtons
        throwingVector = mainCamera.transform.forward * currentThrust;
        isThrowing = true;       
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

        pickupObject = lookObject.GetComponent<PickupObject>();
        if (pickupObject == null)
        {
            Debug.Log("You are trying to pick up something without the PickupObject component on it.");
            return;
        }
        currentlyPickedUpObject = lookObject;
        pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
        pickupRB.constraints = RigidbodyConstraints.FreezeRotation; // TODO necessary?
        pickupObject.objectHolder = this;
        StartCoroutine(pickupObject.PickUp());
    }

}
