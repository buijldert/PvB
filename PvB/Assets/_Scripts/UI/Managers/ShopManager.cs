using UnityEngine;
using UI.Base;
using UI.Controllers;
using UnityEngine.UI;
using TBImplementation.Extensions;
using TBImplementation.ScriptableObjects;
using Components;
using ScriptableObjects;

namespace UI.Managers
{
    public class ShopManager : ScreenManager
    {
        public static ShopManager instance;

        [SerializeField] private MerchandiseData merchandiseData;

        [SerializeField] private InputField codeInput;
        [SerializeField] private Image imageHolder;

        [SerializeField] private Button backButton;

        /// <summary>
        /// Call its base class
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
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

            screenState = MenuState.Shop;
        }

        protected override void PrepareScreen(MenuState _state)
        {
            base.PrepareScreen(_state);
        }

        protected override void OnThemeUpdated(UITheme _UITheme)
        {
            backButton.UpdateButtonTheme(_UITheme.BackButton);
        }

        /// <summary>
        /// Will be called when we enter the screen, and will subscribe to the UI Events.
        /// Also we create the button for the level select.
        /// </summary>
        protected override void StartScreen()
        {
            codeInput.onEndEdit.AddListener((x) => OnCodeInputEnd(x));
            backButton.onClick.AddListener(() => OnBackButtonClicked());
        }

        #region UI Events

        private void OnCodeInputEnd(string _input)
        {
            imageHolder.sprite = merchandiseData.GetUnlockableValue(_input);
        }

        /// <summary>
        /// Will be called when playButton is clicked
        /// </summary>
        private void OnBackButtonClicked()
        {
            UIController.instance.GoToMenuScreen();
        }

        #endregion

        /// <summary>
        /// Will be called when we leave the screen, and will unsubscribe the UI Events and 
        /// destroy the levelselect buttons.
        /// </summary>
        protected override void StopScreen()
        {
            backButton.onClick.RemoveAllListeners();
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
