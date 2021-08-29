using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    [SerializeField] GameObject fireFx;
    [SerializeField] int scoreBonus = 1300;
    [SerializeField] float igniteDelay = 0f;
    [SerializeField] int mischieveContribution = 35;
    public bool isOnFire = false;
    private bool isStartingOnFire = false;
    private bool hasScoreBonus = true;
    Transform otherTransform;

    // on collision 
    // check Fire Tag
    // is one ignited
    //ignite the other

    void Start()
    {
        if (isOnFire)
        {
            isStartingOnFire = true;
            hasScoreBonus = false;
            Ignite();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Fire")
        {
            
            Flammable otherFlammable = other.GetComponentInChildren<Flammable>();
            if (otherFlammable == null) return;

            if (otherFlammable.GetIsOnFire())
            {
                otherTransform = otherFlammable.transform;
                //Ignite();
                Invoke("Ignite", igniteDelay);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        {
            CancelInvoke();
        }
    }


    public bool GetIsOnFire()
    {
        return isOnFire;
    }

    private void Ignite()
    {
        GameObject fx = Instantiate(fireFx, transform.position, Quaternion.identity);
        fx.transform.parent = transform;

        isOnFire = true;

        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/baby/laugh");

        AddToScore();
        

    }

    private void AddToScore()
    {
        if (hasScoreBonus)
            ScoreManager.Instance.AddToScore(scoreBonus);

        MischiefManager.Instance.AddPointsToMischief(mischieveContribution);
    }
}
