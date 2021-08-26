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
    //    MainMusic = FMODUnity.RuntimeManager.CreateInstance("event:/music/gameplay");
    //    MainMusic.start();
    }

    //void Update()
    //{
    //    MainMusic.setParameterByName("mischief", 0f);
    //}

    public void AddPointsToMischief(int mischiefPoints)
    {
        mischiefAmount += mischiefPoints;
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