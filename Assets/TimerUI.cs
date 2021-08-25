using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    public void UpdateTimerUI(float timeToDisplay)
    {
        if (timeToDisplay <= 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:0} : {1:00}", minutes, seconds);
        
    }

}
