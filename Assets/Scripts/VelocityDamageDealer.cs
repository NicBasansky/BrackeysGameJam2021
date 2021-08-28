using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NicLib.Health;
using System;

[RequireComponent(typeof(Rigidbody))]
public class VelocityDamageDealer : MonoBehaviour
{
    [Tooltip("If this is 3, then for every meter per second in velocity you deal 3 times that amount in damage. Higher values makes this a more damaging object. ex: if velocity is 10 then you deal 50 damage")]
    [SerializeField] float damageToVelocityMultiplier = 5;
    [SerializeField] float minVelocityForDamage = 2f;
    float minVelocityForBabyNoise = 2f;
    float boomNoiseVelocityThreshold = 8f;

    Rigidbody rb;
    [SerializeField] int interactableLayerIndex = 6;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/baby/boom");
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == interactableLayerIndex)
        {
            if (rb.velocity.magnitude >= minVelocityForDamage)
            {
                Health health = GetComponent<Health>();
                // TODO find out what the min velocity should be in order to start dealing any damage
                float damage = rb.velocity.magnitude * damageToVelocityMultiplier;
                health.AffectHealth(-damage);
                FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/destroy/explosives");

            }
            
        }
        
        // if (rb.velocity.magnitude > minVelocityForBabyNoise && Random.Range(0, 100) < 60)
        // {
        //     PlayDamageSound();
        // }
    }

    private void PlayDamageSound()
    {
        // bool isHighVelocity = rb.velocity.magnitude > boomNoiseVelocityThreshold;
        // AudioTriggerManager.Instance.PlayPlayerImpactSound(isHighVelocity);
       
    }

}
