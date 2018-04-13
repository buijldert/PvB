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
    public class LevelSelectManager : ScreenManager
    {
        public static LevelSelectManager instance;

        [SerializeField] private LevelDataHolder levelDataHolder;
        [SerializeField] private GameObject levelButton;
        [SerializeField] private Transform contentHolder;

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

            screenState = MenuState.LevelSelect;
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
            backButton.onClick.AddListener(() => OnBackButtonClicked());

            for (int i = 0; i < levelDataHolder.Levels.Length; i++)
            {
                GameObject button = Instantiate(levelButton);
                button.GetComponent<LevelButton>().SetLevelData(levelDataHolder.Levels[i]);
                button.GetComponentInChildren<Text>().text = levelDataHolder.Levels[i].DemoText;
                button.transform.SetParent(contentHolder, false);
            }
        }

        #region UI Events

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

            foreach (Transform child in contentHolder)
            {
                Destroy(child.gameObject);
            }
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
