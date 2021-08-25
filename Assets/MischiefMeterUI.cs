using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MischiefMeterUI : MonoBehaviour
{
    [SerializeField] RectTransform fillRect;

    void Start()
    {
        UpdateMischiefMeter();
    }

    public void UpdateMischiefMeter()
    {
        float percentOfMax = MischiefManager.Instance.GetMischiefPercent();
        fillRect.localScale = new Vector3(1, percentOfMax, 1);
        
    }
}
