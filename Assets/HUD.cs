using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] ScoreUI scoreUI;
    [SerializeField] MischiefMeterUI mischiefMeterUI;
    [SerializeField] TimerUI timerUI;
    [SerializeField] EndScreen endScreen;
    [SerializeField] FirstPersonController controller;


    void Start()
    {
        StartCoroutine(ShowScreen());
    }

    IEnumerator ShowScreen()
    {
        yield return new WaitForSeconds(2f);
        ShowEndScreen(false);
    }

    public void UpdateScoreUI()
    {
        scoreUI.UpdateScoreUI();
    }

    public void UpdateMischiefMeterUI()
    {
        mischiefMeterUI.UpdateMischiefMeter();
    }

    public void UpdateTimerUI(float timerValue)
    {
        timerUI.UpdateTimerUI(timerValue);
    }

    public void ShowEndScreen(bool playerWin)
    {
        controller.SetShouldFreeze(true);
        controller.UnlockCursor();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endScreen.IntializeScreen(playerWin);
        endScreen.gameObject.SetActive(true);

    }

}
