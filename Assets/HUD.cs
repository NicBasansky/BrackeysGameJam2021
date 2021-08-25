using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] ScoreUI scoreUI;
    [SerializeField] MischiefMeterUI mischiefMeterUI;

    public void UpdateScoreUI()
    {
        scoreUI.UpdateScoreUI();
    }

    public void UpdateMischiefMeterUI()
    {
        mischiefMeterUI.UpdateMischiefMeter();
    }
}
