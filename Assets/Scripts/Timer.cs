using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    //private FMOD.Studio.EventInstance musicEvent;

    public float timerSeconds = 90f;
    private bool stopTiming = false;
    [SerializeField] HUD hUD;

    // void Start()
    // {
    //     musicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/music/gameplay");
    //     musicEvent.start();
    // }


    void Update()
    {
        if (stopTiming) return;
        
        if (timerSeconds > 0)
        {
            timerSeconds -= Time.deltaTime;

        }
        else
        {
            timerSeconds = 0;
            Debug.Log("Timer has run out!");

            // musicEvent.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
            // musicEvent.release ();

            AudioTriggerManager.Instance.StopGameplayMusic();

            stopTiming = true;
            GameOverManager.Instance.GameOver(true);
        }
        DisplayTimer(timerSeconds);
    }

    void DisplayTimer(float seconds)
    {
        hUD.UpdateTimerUI(seconds);
    }
}
