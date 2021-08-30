using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using FMOD.Studio;

public class AudioTriggerManager : MMSingleton<AudioTriggerManager>
{
    bool isSpeaking = false;
    float isSpeakingResetSeconds = 2.0f;

    private FMOD.Studio.EventInstance musicEvent;

    void Start()
    {
        
        musicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/music/gameplay");
        musicEvent.start();

    }

    public void StopGameplayMusic()
    {
        musicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicEvent.release();
    }

    public void PlayBabyThrowSound(bool highVelocity)
    {
        if (!isSpeaking)
        {
            if (highVelocity && Random.Range(0, 100) < 60)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/baby/boom");
           
            }
            else if (!highVelocity && Random.Range(0, 100) < 30)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/baby/bored");
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/baby/laugh");
      
            }
            StartCoroutine(ResetIsSpeaking());
        }
    }

    private IEnumerator ResetIsSpeaking()
    {
        yield return new WaitForSeconds(isSpeakingResetSeconds);
        isSpeaking = false;
    }
}
