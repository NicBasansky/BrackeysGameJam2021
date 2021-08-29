using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using NicLib.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Playables;

public class GameOverManager : MMSingleton<GameOverManager>
{
    
    [SerializeField] HUD hUD;
    [SerializeField] Fader fader;
    [SerializeField] float fadeTime = 2f;
    [SerializeField] float endScreenDelay = 2f;
    [SerializeField] FirstPersonController controller;


    
    public void GameOver(bool win)
    {
        controller.SetShouldFreeze(true);
        // calculate and set final score
        ScoreManager.Instance.CalculateFinalScoreWithMischiefBonus();

      

        if (win)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/baby/win");
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/baby/lose");
        }

        fader.FadeToBlack(fadeTime);

        StartCoroutine(ShowEndScreen(win));

    }

    IEnumerator ShowEndScreen(bool win)
    {
        yield return new WaitForSeconds(endScreenDelay);
        hUD.ShowEndScreen(win);
        
    }

   
}
