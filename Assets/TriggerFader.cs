using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NicLib.SceneManagement;

[RequireComponent(typeof(Fader))]
public class TriggerFader : MonoBehaviour
{
    private void Start() {
        Invoke("Fade", 3);
    }

    private void Fade()
    {
        GetComponent<Fader>().FadeToClear(2f);
    }
}
