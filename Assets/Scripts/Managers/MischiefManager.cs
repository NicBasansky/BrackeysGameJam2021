using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;

public class MischiefManager : MMSingleton<MischiefManager>
{
    [FMODUnity.ParamRef]
    string mischief;

    private FMOD.Studio.EventInstance musicEvent;
        
    [SerializeField] float mischiefAmount = 0f;
    [Min(1f)]
    [SerializeField] float maxMischiefAmount = 100f;
    [SerializeField] float mischiefRecoveryIntervalSeconds = 0.4f;
    [SerializeField] float recoveryAmountPerInterval = 1f;
    [SerializeField] HUD hUD;
    private bool maxReached = false;


    void Start()
    {
        mischiefAmount = 0;
        StartCoroutine(ReduceMischiefMeter());

        
        musicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/music/gameplay");
        musicEvent.start();
        
    }
    
    public void AddPointsToMischief(int mischiefPoints)
    {
        mischiefAmount += mischiefPoints;
        if (mischiefAmount >= maxMischiefAmount)
        {
            mischiefAmount = Mathf.Min(maxMischiefAmount, mischiefAmount);
     
            Debug.Log("Maximum Mischief Reached!");
            GameOverManager.Instance.GameOver(false);
            maxReached = true;

            musicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicEvent.release();
        }
        
        hUD.UpdateMischiefMeterUI();
        CheckMischiefPercentForAdaptiveMusic();
    }

    public bool GetIsMaxReached()
    {
        return maxReached;
    }


    private void CheckMischiefPercentForAdaptiveMusic()
    {
         float percent = GetMischiefPercent();
         FMODUnity.RuntimeManager.StudioSystem.setParameterByName("mischief", GetMischiefPercent());
    }
    

    public float GetMischiefPercent()
    {
        return mischiefAmount / maxMischiefAmount;
    }

    IEnumerator ReduceMischiefMeter()
    {
        while (true)
        {
            yield return new WaitForSeconds(mischiefRecoveryIntervalSeconds);

            DeductPointsFromMischief();
        }
    }

    private void DeductPointsFromMischief()
    {
        mischiefAmount -= recoveryAmountPerInterval;
        mischiefAmount = Mathf.Max(mischiefAmount, 0);
        hUD.UpdateMischiefMeterUI();
    }
}
