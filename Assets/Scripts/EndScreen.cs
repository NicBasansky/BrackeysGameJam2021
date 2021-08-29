using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject winTextGo;
    [SerializeField] GameObject loseTextGo;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI lowMischiefScoreText;
    

    void Start()
    {
        //gameObject.SetActive(false);
    }

    public void IntializeScreen(bool playerWin)
    {
        if (playerWin)
        {
            winTextGo.SetActive(true);
            loseTextGo.SetActive(false);
        }
        else
        {
            winTextGo.SetActive(false);
            loseTextGo.SetActive(true);
        }
    }

    public void RestartScene()
    {
        Invoke("LoadScene", 1.5f);
    }

    private void LoadScene()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

    }

    public void UpdateScore()
    {
        moneyText.text = "$" + ScoreManager.Instance.GetTotalDestructionMoneyValue() + ".00";
        totalScoreText.text = ScoreManager.Instance.GetScore().ToString();
        lowMischiefScoreText.text = ScoreManager.Instance.GetMischiefScoreBonus().ToString();
    }
}
