using UnityEngine;
using UI.Base;
using UI.Controllers;
using UnityEngine.UI;
using TBImplementation.Extensions;
using TBImplementation.ScriptableObjects;

namespace UI.Managers
{
    public class MenuManager : ScreenManager
    {
        public static MenuManager instance;

        [SerializeField] private Button playButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button musicButton;

        /// <summary>
        /// Sunscribes to the UI events we want to use, and calls its base class
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeToEvents();
        }

        /// <summary>
        /// Makes an instance of our class and sets the MenuState in represents
        /// </summary>
        protected override void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

            screenState = MenuState.Menu;
        }

        /// <summary>
        /// Made to eliminate the need to type de same events twice.
        /// </summary>
        private void SubscribeToEvents()
        {
            playButton.onClick.AddListener(() => OnPlayButtonClicked());
            shopButton.onClick.AddListener(() => OnShopButtonClicked());
            settingsButton.onClick.AddListener(() => OnSettingsButtonClicked());
            musicButton.onClick.AddListener(() => OnMusicButtonClicked());
        }

        protected override void PrepareScreen(MenuState _state)
        {
            base.PrepareScreen(_state);
        }

        protected override void OnThemeUpdated(UITheme _UITheme)
        {
            playButton.UpdateButtonTheme(_UITheme.ButtonPlay);
            shopButton.UpdateButtonTheme(_UITheme.ButtonShop);
            settingsButton.UpdateButtonTheme(_UITheme.ButtonSettings);
            musicButton.UpdateButtonTheme(_UITheme.ButtonMusic);
        }

        /// <summary>
        /// Will be called when we enter the screen, and will subscribe to the UI Events
        /// </summary>
        protected override void StartScreen()
        {
            SubscribeToEvents();
        }

        #region UI Events

        /// <summary>
        /// Will be called when playButton is clicked
        /// </summary>
        private void OnPlayButtonClicked()
        {
            UIController.instance.GoToLevelSelectScreen();
        }

        /// <summary>
        /// Will be called when shopButton is clicked
        /// </summary>
        private void OnShopButtonClicked()
        {
            UIController.instance.GoToShopScreen();
        }

        /// <summary>
        /// Will be called when settingsButton is clicked
        /// </summary>
        private void OnSettingsButtonClicked()
        {
            // TODO: Animate music button in when this button is clicked
        }

        /// <summary>
        /// Will be called when musicButton is clicked
        /// </summary>
        private void OnMusicButtonClicked()
        {
            // TODO: Make this function actually turn on/off music
        }

        #endregion

        /// <summary>
        /// Will be called when we leave the screen, and will unsubscribe the UI Events
        /// </summary>
        protected override void StopScreen()
        {
            playButton.onClick.RemoveAllListeners();
            shopButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            musicButton.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// Calls its base method, to unsubscribe from the UI System
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
