using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreValue;

    void Start()
    {
        scoreValue.text = "0";
    }

    public void UpdateScoreUI()
    {
        scoreValue.text = ScoreManager.Instance.GetScore().ToString();
    }
}
