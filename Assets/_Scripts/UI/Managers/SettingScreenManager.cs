using UnityEngine;
using UnityEngine.UI;
using RR.UI.Base;
using RR.Controllers;
using RR.UI.Controllers;
using DG.Tweening;
using RR.Managers;

namespace RR.UI.Managers
{
    /// <summary>
    /// This class controlls the UI Elements on the Settings-Screen
    /// </summary>
    public class SettingScreenManager : ScreenManager
    {
        public static SettingScreenManager instance;

        private enum ButtonType
        {
            Sound,
            Vibration,
            Credits
        }

        [SerializeField] private Button[] settingsButtons;

        private Text soundButtonText;
        private Text vibrationButtonText;
        private Sequence startUpSequence;

        /// <summary>
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            settingsButtons[(int)ButtonType.Sound].onClick.AddListener(() => OnSoundButtonClicked());
            settingsButtons[(int)ButtonType.Vibration].onClick.AddListener(() => OnVibrationButtonClicked());
            settingsButtons[(int)ButtonType.Credits].onClick.AddListener(() => OnCreditsButtonClicked());
        }

        /// <summary>
        /// Awake is called before Start() and OnEnable().
        /// </summary>
        protected override void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

            screenState = MenuState.Settings;
        }

        /// <summary>
        /// Start() is called after Awake() and OnEnable().
        /// </summary>
        private void Start()
        {
            soundButtonText = settingsButtons[(int)ButtonType.Sound].GetComponentInChildren<Text>();
            vibrationButtonText = settingsButtons[(int)ButtonType.Vibration].GetComponentInChildren<Text>();
            soundButtonText.text = (SettingsController.GetMuteState()) ? "Sound: On" : "Sound: Off";
            vibrationButtonText.text = (SettingsController.GetVibrationState()) ? "Vibration: On" : "Vibration: Off";
        }

        /// <summary>
        /// Will be called when we are on this particular screen
        /// </summary>
        protected override void StartScreen()
        {
            DoStartupAnimation();
        }

        /// <summary>
        /// This function will do the start animation of this screen
        /// </summary>
        private void DoStartupAnimation()
        {
            startUpSequence = DOTween.Sequence();
            for (int i = settingsButtons.Length - 1; i >= 0; i--)
            {
                RectTransform rect = settingsButtons[i].GetComponent<RectTransform>();
                Text buttonText = settingsButtons[i].GetComponentInChildren<Text>();

                rect.localScale = new Vector3(1, 1, 1);
                rect.anchoredPosition = new Vector2(0, 750 - (i * 450));

                startUpSequence.Append(settingsButtons[i].GetComponent<Image>().DOFade(1, 0.75f));
                startUpSequence.Join(buttonText.DOFade(1, 0.75f));
            }
        }

        #region UI Events
        /// <summary>
        /// Will fire when the sound-button is clicked
        /// </summary>
        private void OnSoundButtonClicked()
        {
            SettingsController.SetMute(!SettingsController.GetMuteState());
            VolumeManager.instance.ChangeMasterVolume();

            soundButtonText.text = (SettingsController.GetMuteState()) ? "Sound: On" : "Sound: Off";
        }

        /// <summary>
        /// Will fire when the vibration-button is clicked
        /// </summary>
        private void OnVibrationButtonClicked()
        {
            SettingsController.SetVibration(!SettingsController.GetVibrationState());

            vibrationButtonText.text = (SettingsController.GetVibrationState()) ? "Vibration: On" : "Vibration: Off";
        }

        /// <summary>
        /// Will fire when the credits-button is clicked
        /// </summary>
        private void OnCreditsButtonClicked()
        {

        }
        #endregion // UI Events

        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected override void StopScreen()
        {
            startUpSequence.Kill();

            for (int i = settingsButtons.Length - 1; i >= 0; i--)
            {
                Text buttonText = settingsButtons[i].GetComponentInChildren<Text>();

                settingsButtons[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                buttonText.color = new Color(1, 1, 1, 0);
            }
        }

        /// <summary>
        /// OnDisable is called before the object is disabled.
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();

            settingsButtons[(int)ButtonType.Sound].onClick.RemoveAllListeners();
            settingsButtons[(int)ButtonType.Vibration].onClick.RemoveAllListeners();
            settingsButtons[(int)ButtonType.Credits].onClick.RemoveAllListeners();
        }
    }
}
