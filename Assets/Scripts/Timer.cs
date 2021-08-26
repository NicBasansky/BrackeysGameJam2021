using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timerSeconds = 90f;
    [SerializeField] HUD hUD;

    void Update()
    {
        if (timerSeconds > 0)
        {
            timerSeconds -= Time.deltaTime;

        }
        else
        {
            timerSeconds = 0;
            Debug.Log("Timer has run out!");
        }
        DisplayTimer(timerSeconds);
    }

    void DisplayTimer(float seconds)
    {
        hUD.UpdateTimerUI(seconds);
    }
}
