using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject winTextGo;
    [SerializeField] GameObject loseTextGo;

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
}
