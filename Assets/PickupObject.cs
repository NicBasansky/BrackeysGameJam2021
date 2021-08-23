using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [HideInInspector] public ObjectHolder objectHolder;
    [HideInInspector] public bool pickedUp = false;
    public float breakForce = 35f;
    public float waitOnPickup = 0.2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (pickedUp)
        {
            if (collision.relativeVelocity.magnitude > breakForce)
            {
                objectHolder.BreakConnection();
            }
        }
    }
    public IEnumerator PickUp()
    {
        // This prevents the object interacting with the ground as soon as you pick it up
        yield return new WaitForSecondsRealtime(waitOnPickup);
        pickedUp = true;
    }
}
