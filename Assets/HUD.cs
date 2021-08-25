using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] ScoreUI scoreUI;
    [SerializeField] MischiefMeterUI mischiefMeterUI;
    [SerializeField] TimerUI timerUI;

    public void UpdateScoreUI()
    {
        scoreUI.UpdateScoreUI();
    }

    public void UpdateMischiefMeterUI()
    {
        mischiefMeterUI.UpdateMischiefMeter();
    }

    public void UpdateTimerUI(float timerValue)
    {
        timerUI.UpdateTimerUI(timerValue);
    }
}
