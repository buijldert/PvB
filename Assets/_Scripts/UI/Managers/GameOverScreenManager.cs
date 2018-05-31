using System;
using System.Collections;
using System.Collections.Generic;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class GameOverScreenManager : MonoBehaviour 
{
    public static GameOverScreenManager instance;

    [SerializeField] private Button resume;
   
    [SerializeField] private Button quit;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Image background;
    [SerializeField] private GameObject menu;
    [SerializeField] private Text score;

    public static Action onQuit;

    private void OnEnable()
    {
        resume.onClick.AddListener(() => OnResumeButtonClicked());
        quit.onClick.AddListener(() => OnQuitButtonClicked());
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    public void UpdateScore()
    {
        score.text = GameviewManager.instance.GetScore().ToString();
    }

    private void OnResumeButtonClicked()
    {
        HomeManager.instance.StartGame();
        gameOverScreen.SetActive(false);
        GameviewManager.instance.ResetScore();
    }

    private void OnQuitButtonClicked()
    {
        UIController.instance.GoToHomeScreen();
        gameOverScreen.SetActive(false);
        background.gameObject.SetActive(true);
        menu.SetActive(true);
        GameviewManager.instance.ResetScore();


        if (onQuit != null)
        {
            onQuit();
        }
    }
}
