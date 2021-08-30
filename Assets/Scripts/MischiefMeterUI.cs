using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MischiefMeterUI : MonoBehaviour
{
    [SerializeField] RectTransform fillRect;

    void Start()
    {
        fillRect.localScale = new Vector3(1, 0, 1);
    }

    public void UpdateMischiefMeter()
    {
        float percentOfMax = MischiefManager.Instance.GetMischiefPercent();
        if (percentOfMax != 0)
        {
            fillRect.localScale = new Vector3(1, percentOfMax, 1);

        }
        else
        {
            fillRect.localScale = new Vector3(1, 0, 1);
        }
        
    }
}
