using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

public class AudioTriggerManager : MMSingleton<AudioTriggerManager>
{
    bool isSpeaking = false;
    float isSpeakingResetSeconds = 2.0f;

    // void Start()
    // {
    //     StartCoroutine(PlaySound());
    // }

    // IEnumerator PlaySound()
    // {

    //     {
    //         yield return new WaitForSeconds(2f);
    //         PlayDamageSound();
    //         yield return new WaitForSeconds(2f);
    //         PlayDamageSound();
    //         yield return new WaitForSeconds(2f);
    //         PlayDamageSound();
    //         yield return new WaitForSeconds(2f);
    //         PlayDamageSound();
    //         yield return new WaitForSeconds(2f);
    //         PlayDamageSound();
    //     }
    // }

    public void PlayBabyThrowSound(bool highVelocity)
    {
        if (!isSpeaking)
        {
            if (highVelocity && Random.Range(0, 100) < 60)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/baby/boom");
                print("boom");
            }
            else if (!highVelocity && Random.Range(0, 100) < 30)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/baby/bored");
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/baby/laugh");
                print("laugh");
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
