using System;
using UnityEngine;
using UnityEngine.UI;
using RR.Controllers;
using RR.UI.Controllers;
using RR.Managers;

namespace RR.UI.Managers
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
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        private void OnEnable()
        {
            resume.onClick.AddListener(() => OnResumeButtonClicked());
            sound.onClick.AddListener(() => OnSoundButtonClicked());
            vibration.onClick.AddListener(() => OnVibrationButtonClicked());
            quit.onClick.AddListener(() => OnQuitButtonClicked());
        }

        /// <summary>
        /// Awake() is called before Start() and OnEnable().
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
            GameController.instance.ResumeGame();
        }

        /// <summary>
        /// Will fire when the sound-button is clicked.
        /// </summary>
        private void OnSoundButtonClicked()
        {
            SettingsController.SetMute(!SettingsController.GetMuteState());
            VolumeManager.instance.ChangeMasterVolume();
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

            GameController.instance.StopGame();
            GameviewManager.instance.ResetScore();
            UIController.instance.GoToHomeScreen();
        }
        #endregion // UI Events

        /// <summary>
        /// OnDisable() is called before the object is disabled.
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