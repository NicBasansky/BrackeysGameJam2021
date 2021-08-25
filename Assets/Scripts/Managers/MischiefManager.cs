using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;

public class MischiefManager : MMSingleton<MischiefManager>
{
    [SerializeField] float mischiefAmount = 0f;
    [Min(1f)]
    [SerializeField] float maxMischiefAmount = 100f;
    [SerializeField] float mischiefRecoveryIntervalSeconds = 0.4f;
    [SerializeField] float recoveryAmountPerInterval = 3f;
    [SerializeField] HUD hUD;


    void Start()
    {
        StartCoroutine(ReduceMischiefMeter());
    }

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
        if (percent <= .1f)
        {
            // play music that is under 10%
        }
        else if (percent <= .5f)
        {
            // play music that is under 50% but above 10%
        }
        else if (percent <= .75)
        {
            // play music that is under 75% but above 50%
        }
        else
        {
            // play music that is above 75%
        }
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
