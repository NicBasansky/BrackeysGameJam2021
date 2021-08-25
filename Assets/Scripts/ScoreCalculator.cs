using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectProperties
{
    public float moneyValue;
    [Tooltip("How many mischieve points when this object is destroyed")]
    [Range(0, 30)]
    public int mischiefMeterPointsOnDestruction;
    public int damageToScoreMultiplier = 2;
    public int bonusOnDestruction = 100;
}

public class ScoreCalculator : MonoBehaviour
{
    public ObjectProperties properties;

    // add to score for each point of damage inflicted on itself
    public void AddToScore_Damage(float damage)
    {
        ScoreManager.Instance.AddToScore((int)(damage * properties.damageToScoreMultiplier));
    }

    // add bonus if this object destroyed another
    public void AddDestructionBonus()
    {
        ScoreManager.Instance.AddToScore(properties.bonusOnDestruction);
    }

    // when this object is destroyed add to score relative to money value
    public void AddToScore_moneyValue()
    {
        ScoreManager.Instance.AddToTotalMoneyValue(properties.moneyValue);
    }

    public void AddPointsToMischiefOnDestruction()
    {
        MischiefManager.Instance.AddPointsToMischief(properties.mischiefMeterPointsOnDestruction);
    }
}
