using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    [SerializeField] GameObject infoPanel;

    public void StartGame()
    {
        if (FMODUnity.RuntimeManager.HasBankLoaded("Master"))
        {
            Debug.Log("Master Bank Loaded");
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }

    public void OpenInfoPanel()
    {
        infoPanel.SetActive(true);
    }
}
