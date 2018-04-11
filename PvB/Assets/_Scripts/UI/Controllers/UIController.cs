using UnityEngine;
using System.Linq;
using System;
using TBImplementation.ScriptableObjects;

namespace UI.Controllers
{
    public enum MenuState
    {
        Menu,
        LevelSelect,
        Shop,
        Gameview,
    }

    [ExecuteInEditMode]
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public static Action<MenuState> OnScreenChanged;
        public static Action<UITheme> OnThemeChanged;

        [SerializeField] private UISettings settings;
        [SerializeField] private GameObject[] holders;

        /// <summary>
        /// In this method we register to the events we use to give live feedback in the editor
        /// </summary>
        private void OnEnable()
        {
            #if UNITY_EDITOR
            settings.RegisterListener(UpdateTheme);
            settings.Theme.RegisterListener(UpdateTheme);
            #endif
        }

        /// <summary>
        /// Makes an instance of our class
        /// </summary>
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;
        }

        /// <summary>
        /// Update the team when the game starts
        /// </summary>
        private void Start()
        {
            UpdateTheme();
        }

        /// <summary>
        /// Sets the state of the UI system and activates and deactivates the holders.
        /// </summary>
        /// <param name="state">The MenuState we want to set.</param>
        private void SetState(MenuState state)
        {
            TurnHoldersInactive();
            holders[(int)state].SetActive(true);

            if(OnScreenChanged != null)
            {
                OnScreenChanged(state);
            }
        }

        /// <summary>
        /// Sends the OnThemeChanged event to update all screens.
        /// </summary>
        public void UpdateTheme()
        {
            if (OnThemeChanged != null)
            {
                OnThemeChanged(settings.Theme);
            }
        }

        /// <summary>
        /// Turns the holders we don't want to show inactive.
        /// </summary>
        private void TurnHoldersInactive()
        {
            foreach (GameObject holder in holders.Where(holder => holder.activeSelf))
            {
                holder.SetActive(false);
            }
        }

        #region GoTo methods
        public void GoToMenuScreen()
        {
            SetState (MenuState.Menu);
        }

        public void GoToLevelSelectScreen()
        {
            SetState (MenuState.LevelSelect);
        }
    
        public void GoToShopScreen()
        {
            SetState(MenuState.Shop);
        }
        public void GoToGameScreen()
        {
            SetState(MenuState.Gameview);
        }
        #endregion
    }
}


