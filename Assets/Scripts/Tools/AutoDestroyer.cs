using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NicLib.Utilities
{
    public class AutoDestroyer : MonoBehaviour
    {
        [SerializeField] float destroyDelay = 5f;


        void Start()
        {
            Destroy(this.gameObject, destroyDelay);
        }

    }

}
