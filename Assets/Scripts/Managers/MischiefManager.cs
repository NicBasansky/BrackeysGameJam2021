using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;

public class MischiefManager : MMSingleton<MischiefManager>
{
    [FMODUnity.ParamRef]
    string mischief;

    //private FMOD.Studio.EventInstance musicEvent;
        
    [SerializeField] float mischiefAmount = 0f;
    [Min(1f)]
    [SerializeField] float maxMischiefAmount = 100f;
    [SerializeField] float mischiefRecoveryIntervalSeconds = 0.4f;
    [SerializeField] float recoveryAmountPerInterval = 1f;
    [SerializeField] HUD hUD;
    private bool maxReached = false;

    void Start()
    {
        //mischiefAmount = 0;
        StartCoroutine(ReduceMischiefMeter());
        
    }
    
    public void AddPointsToMischief(int mischiefPoints)
    {
        mischiefAmount += mischiefPoints;
        CheckIfGameEnds();

        hUD.UpdateMischiefMeterUI();
        CheckMischiefPercentForAdaptiveMusic();
    }

    private void CheckIfGameEnds()
    {
        if (mischiefAmount >= maxMischiefAmount)
        {
            mischiefAmount = Mathf.Min(maxMischiefAmount, mischiefAmount);

            Debug.Log("Maximum Mischief Reached!");
            GameOverManager.Instance.GameOver(false);
            maxReached = true;

            AudioTriggerManager.Instance.StopGameplayMusic();
        }
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
        if (mischiefAmount <= 0)
            return 0;
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
