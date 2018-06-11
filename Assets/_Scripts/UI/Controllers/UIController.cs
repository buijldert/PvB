using UnityEngine;
using System.Linq;
using System;
using UnityEngine.PostProcessing;

namespace RR.UI.Controllers
{
    public enum MenuState
    {
        Home,
        Shop,
        Code,
        Settings,
        GameView,
    }

    /// <summary>
    /// This class is responsible for controlling the different States and Screens.
    /// </summary>
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public static Action<MenuState> OnScreenChanged;

        [SerializeField] private GameObject[] holders;

        [Header("Global UI Elements")]
        [SerializeField] private GameObject background;

        [Tooltip("This screen should be in Camera space, not in screen space")]
        [SerializeField] private GameObject codeScreen;

        [Header("Menu")]
        [SerializeField] private GameObject menu;

        // TODO:Should be moved to a GameController class
        [Header("Gameplay Elements")]
        [SerializeField] private GameObject controller;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject objectPool;
        [SerializeField] private GameObject gameManager;
        [SerializeField] private PostProcessingBehaviour postProcessingBehaviour; 

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

        /// <summary>
        /// Sets the MenuState enum to the current state.
        /// Also sends an Action with te current MenuState to the ScreenManager BaseClass.
        /// </summary>
        /// <param name="state">State we want the system to be in.</param>
        public void SetState(MenuState _state)
        {
            TurnHoldersInactive();
            holders[(int)_state].SetActive(true);

            if (OnScreenChanged != null)
            {
                OnScreenChanged(_state);
            }
        }

        /// <summary>
        /// Turns the holders which are not used inactive.
        /// </summary>
        private void TurnHoldersInactive()
        {
            foreach (GameObject holder in holders.Where(holder => holder.activeSelf))
            {
                holder.SetActive(false);
            }
        }

        /// <summary>
        /// Activates the screen elements that correspond with the code activation screen.
        /// </summary>
        /// <param name="_isActive">Whether the screen should be set active.</param>
        private void ActivateCodeScreenElements(bool _isActive)
        {
            background.SetActive(!_isActive);
            codeScreen.SetActive(_isActive);
            controller.SetActive(!_isActive);
            postProcessingBehaviour.enabled = !_isActive;
        }

        /// <summary>
        /// Activates the screen elements that correspond with the game screen.
        /// </summary>
        /// <param name="_isActive">Whether the screen should be set active.</param>
        private void ActivateGameViewElements(bool _isActive)
        {
            background.gameObject.SetActive(!_isActive);
            gameManager.SetActive(_isActive);
            objectPool.SetActive(_isActive);
            menu.SetActive(!_isActive);
            player.SetActive(_isActive);
        }

        #region GoTo methods

        /// <summary>
        /// Goes to the HomeScreen
        /// </summary>
        public void GoToHomeScreen()
        {
            SetState(MenuState.Home);
            ActivateGameViewElements(false);
            ActivateCodeScreenElements(false);
        }

        /// <summary>
        /// Goes to the ShopScreen
        /// </summary>
        public void GoToShopScreen()
        {
            SetState(MenuState.Shop);
            ActivateCodeScreenElements(false);
        }

        /// <summary>
        /// Goes to the CodeScreen
        /// </summary>
        public void GoToCodeScreen()
        {
            SetState(MenuState.Code);
            ActivateCodeScreenElements(true);
        }

        /// <summary>
        /// Goes to the SettingScreen
        /// </summary>
        public void GoToSettingsScreen()
        {
            SetState(MenuState.Settings);
            ActivateCodeScreenElements(false);
        }

        /// <summary>
        /// Goes to the GameView
        /// </summary>
        public void GoToGameView()
        {
            SetState(MenuState.GameView);
            ActivateGameViewElements(true);
        }

        #endregion
    }
}


