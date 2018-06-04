<<<<<<< HEAD
﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UI.Controllers;
using Utility;

namespace UI.Managers
{
    /// <summary>
    /// This class controlls the UI Elements on the Pause-Screen
    /// </summary>
    public class PauseScreenManager : MonoBehaviour
    {
        public static PauseScreenManager instance;

        [SerializeField] private Button resume;
        [SerializeField] private Button sound;
        [SerializeField] private Button vibration;
        [SerializeField] private Button quit;

        [SerializeField] private GameObject pauseScreen;

        public static Action onQuit;

        /// <summary>
        /// Subscribes to the events we want to react on
        /// </summary>
        private void OnEnable()
        {
            resume.onClick.AddListener(() => OnResumeButtonClicked());
            sound.onClick.AddListener(() => OnSoundButtonClicked());
            vibration.onClick.AddListener(() => OnVibrationButtonClicked());
            quit.onClick.AddListener(() => OnQuitButtonClicked());
        }

        /// <summary>
        /// Singleton implementation
        /// </summary>
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;
        }


        #region UI Events
        /// <summary>
        /// Will fire when the resume-button is clicked.
        /// </summary>
        private void OnResumeButtonClicked()
        {
            pauseScreen.SetActive(false);
            PauseGameManager.instance.ResumeGame();
        }

        /// <summary>
        /// Will fire when the sound-button is clicked.
        /// </summary>
        private void OnSoundButtonClicked()
        {
            SettingsController.SetMute(!SettingsController.GetMuteState());
        }

        /// <summary>
        /// Will fire when the vibration-button is clicked.
        /// </summary>
        private void OnVibrationButtonClicked()
        {
            SettingsController.SetVibration(!SettingsController.GetVibrationState());
        }

        /// <summary>
        /// Will fire when the quit-button is clicked.
        /// </summary>
        private void OnQuitButtonClicked()
        {
            pauseScreen.SetActive(false);

            GameviewManager.instance.ResetScore();
            UIController.instance.GoToHomeScreen();

            if (onQuit != null)
            {
                onQuit();
            }
        }
        #endregion // UI Events

        /// <summary>
        /// Unsubscribes from the events that we used
        /// </summary>
        private void OnDisable()
        {
            resume.onClick.RemoveAllListeners();
            sound.onClick.RemoveAllListeners();
            vibration.onClick.RemoveAllListeners();
            quit.onClick.RemoveAllListeners();
        }
    }
}
=======
﻿using System.Collections;
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
>>>>>>> Development_branch
