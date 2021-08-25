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
    }

    public void DeductScoreForMaxingMischief()
    {
        score -= scoreDeductionFromMaxingMischief;
        CalculateScore();
    }

    public int CalculateScore()
    {
        int scoreFromMoneyValue = (int)totalDestructionMoneyValue * pointsPerDollar;
        // score deduction from having lost time from reaching 100% mischief?

        score = score + scoreFromMoneyValue;

        hUD.UpdateScoreUI();

        return score; // need to return?
    }

    // needs to be called at the end of the game
    private int CalculateFinalScoreWithMischiefBonus()
    {
        int scoreFromMischief = (int)(100 - MischiefManager.Instance.GetMischiefPercent()) * pointsPerPercentNotMischief;
        score = score + scoreFromMischief;

        hUD.UpdateScoreUI();

        return score;
    }
}
