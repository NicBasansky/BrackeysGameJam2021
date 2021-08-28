using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;

public class MischiefManager : MMSingleton<MischiefManager>
{
    [FMODUnity.ParamRef]
    string mischief;
        
    [SerializeField] float mischiefAmount = 0f;
    [Min(1f)]
    [SerializeField] float maxMischiefAmount = 100f;
    [SerializeField] float mischiefRecoveryIntervalSeconds = 0.4f;
    [SerializeField] float recoveryAmountPerInterval = 1f;
    [SerializeField] HUD hUD;


    void Start()
    {
        StartCoroutine(ReduceMischiefMeter());
    }
    
    public void AddPointsToMischief(int mischiefPoints)
    {
        mischiefAmount += mischiefPoints;
        if (mischiefAmount >= maxMischiefAmount)
        {
            mischiefAmount = Mathf.Min(maxMischiefAmount, mischiefAmount);
            // TODO call event that Max Mischief if reached
            Debug.Log("Maximum Mischief Reached!");
            GameOverManager.Instance.GameOver();
        }
        
        hUD.UpdateMischiefMeterUI();
        CheckMischiefPercentForAdaptiveMusic();
    }

    // TODO GUS SOUNDS
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
