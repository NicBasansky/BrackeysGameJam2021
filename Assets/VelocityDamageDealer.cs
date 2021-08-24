using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NicLib.Health;

[RequireComponent(typeof(Rigidbody))]
public class VelocityDamageDealer : MonoBehaviour
{
    [Tooltip("If this is 3, then for every meter per second in velocity you deal 3 times that amount in damage. Higher values makes this a more damaging object. ex: if velocity is 10 then you deal 50 damage")]
    [SerializeField] float damageToVelocityMultiplier = 5;

    Rigidbody rb;
    [SerializeField] int interactableLayerIndex = 6;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == interactableLayerIndex)
        {
            // TODO find out what the min velocity should be in order to start dealing any damage
            Health health = GetComponent<Health>();
            health.AffectHealth(-rb.velocity.magnitude * damageToVelocityMultiplier);
        }
    }
}
