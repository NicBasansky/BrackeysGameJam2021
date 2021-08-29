using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using FMOD.Studio;

public class AudioTriggerManager : MMSingleton<AudioTriggerManager>
{
    bool isSpeaking = false;
    float isSpeakingResetSeconds = 2.0f;


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
