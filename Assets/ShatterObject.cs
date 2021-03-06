using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterObject : MonoBehaviour
{
    [SerializeField] float explosionForce = 8f;
    [SerializeField] float explosionRadius = 3f;
    [Tooltip("This is useful for things like a base for the sculpture to sit on")]
    [SerializeField] GameObject[] objectsToDetatchAddRB;
    [SerializeField] Collider colliderToOff;


    private bool shatter = false;


    private void FixedUpdate() 
    {
        if (!shatter) return;
        MeshRenderer rend = GetComponent<MeshRenderer>();
        if (rend)
        {
            rend.enabled = false;
        }
        
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
            MeshCollider coll = t.gameObject.AddComponent<MeshCollider>();
            coll.convex = true;
            //t.GetComponent<MeshCollider>().isTrigger = false;

            Rigidbody rb = t.gameObject.AddComponent<Rigidbody>();
            //Rigidbody rb = t.GetComponent<Rigidbody>();
            if (rb)
            {
                //rb.constraints = RigidbodyConstraints.None;
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1f, ForceMode.Impulse);

            }
            t.transform.parent = null;

        }

        shatter = false;
    }

    public void Shatter()
    {
        // remove collider on base gameObject
        if (colliderToOff)
        {
            colliderToOff.enabled = false;

        }

        UnparentObjectsAndAddRB();
        shatter = true;
        
    }

    private void UnparentObjectsAndAddRB()
    {
        if (objectsToDetatchAddRB.Length == 0)
            return;

        foreach(var go in objectsToDetatchAddRB)
        {
            go.transform.parent = null;
            go.AddComponent<Rigidbody>();
        }
    }
}
