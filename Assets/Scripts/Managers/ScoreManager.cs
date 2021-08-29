using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;

public class ScoreManager : MMSingleton<ScoreManager>
{
    [SerializeField] int score = 0;
    [SerializeField] float totalDestructionMoneyValue = 0;
    [SerializeField] int pointsPerDollar = 7;
    [Tooltip("Ex. If mischief is 60/100 then 40 (100-60 = 40) times this number to get a score from mischief")]
    [SerializeField] int pointsPerPercentNotMischief = 3;
    [SerializeField] int scoreDeductionFromMaxingMischief = 50;
    [SerializeField] HUD hUD;
    [SerializeField] EndScreen endScreen;
    private int finalMishiefScore = 0;

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreAddition)
    {
        score += scoreAddition;
        CalculateScore();

    }

    public void AddToTotalMoneyValue(float moneyAddition)
    {
        totalDestructionMoneyValue += moneyAddition;
        CalculateScore();
        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/cash/mid");
    }

    public void DeductScoreForMaxingMischief()
    {
        score -= scoreDeductionFromMaxingMischief;
        CalculateScore();
    }

    public int GetTotalDestructionMoneyValue()
    {
        return Mathf.FloorToInt(totalDestructionMoneyValue);
    }

    public int CalculateScore()
    {
        int scoreFromMoneyValue = (int)totalDestructionMoneyValue * pointsPerDollar;
        // score deduction from having lost time from reaching 100% mischief?

        score = score + scoreFromMoneyValue;

        hUD.UpdateScoreUI();

        return score; // need to return?
    }

    public int GetMischiefScoreBonus()
    {
        return finalMishiefScore;
    }

    
    public void CalculateFinalScoreWithMischiefBonus()
    {
        int scoreFromMischief = (100 - Mathf.FloorToInt(MischiefManager.Instance.GetMischiefPercent())) * pointsPerPercentNotMischief;
        print("scoreFromMischief: " + scoreFromMischief + " score: " + score);
        score = score + scoreFromMischief;
        finalMishiefScore = scoreFromMischief;

        endScreen.UpdateScore();
        //hUD.UpdateScoreUI();

    
    }
}
