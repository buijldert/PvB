using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using UI.Controllers;
using System;

public class PauseScreenManager : MonoBehaviour 
{
    public static PauseScreenManager instance;

    [SerializeField] private Button resume;
    [SerializeField] private Button sound;
    [SerializeField] private Button vibration;
    [SerializeField] private Button quit;

    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Image background;
    [SerializeField] private GameObject menu;

    public static Action onQuit;

    private void OnEnable()
    {
        resume.onClick.AddListener(() => OnResumeButtonClicked());
        sound.onClick.AddListener(() => OnSoundButtonClicked());
        vibration.onClick.AddListener(() => OnVibrationButtonClicked());
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

    private void OnResumeButtonClicked()
    {
        //PauseGameManager.instance.ResumeGame();
        pauseScreen.SetActive(false);

    }

    private void OnSoundButtonClicked()
    {
        SettingsController.SetMute(!SettingsController.GetMuteState());
    }

    private void OnVibrationButtonClicked()
    {
        SettingsController.SetVibration(!SettingsController.GetVibrationState());
    }

    private void OnQuitButtonClicked()
    {
        UIController.instance.GoToHomeScreen();
        pauseScreen.SetActive(false);
        background.gameObject.SetActive(true);
        menu.SetActive(true);

        if(onQuit != null)
        {
            onQuit();
        }
    }
}
