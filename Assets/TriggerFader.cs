using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NicLib.SceneManagement;

[RequireComponent(typeof(Fader))]
public class TriggerFader : MonoBehaviour
{
    [SerializeField] float fadeTriggerDelay = 2.2f;
    private void Start() 
    {
        Invoke("Fade", fadeTriggerDelay);
    }

    private void Fade()
    {
        GetComponent<Fader>().FadeToClear(2f);
    }
}
