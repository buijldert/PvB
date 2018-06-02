using System;
using UnityEngine;
using UnityEngine.UI;
using UI.Base;
using UI.Controllers;
using DG.Tweening;

namespace UI.Managers
{
    /// <summary>
    /// This class controlls the UI Elements on the Home-Screen
    /// </summary>
    public class HomeScreenManager : ScreenManager
    {
        public static HomeScreenManager instance;

        public static Action OnRestartGame;

        [SerializeField] private Button startbutton;
        [SerializeField] private Image logo;

        /// <summary>
        /// Subscribes to different events we want react on
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            startbutton.onClick.AddListener(() => OnStartButtonClicked());
            MenuManager.onOpeningSequenceEnded += DoAnimation;
        }

        /// <summary>
        /// Singleton Implementation.
        /// Also sets the screenstate to the state this script represents.
        /// </summary>
        protected override void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

            screenState = MenuState.Home;
        }

        /// <summary>
        /// Will be called when we are on this particular screen
        /// </summary>
        protected override void StartScreen()
        {
            //
        }

        #region DOTween Animations
        /// <summary>
        /// Fades in the Logo and start-button
        /// </summary>
        public void DoAnimation()
        {
            Sequence s = DOTween.Sequence();
            s.Append(startbutton.GetComponent<Image>().DOFade(1, 1));
            s.Join(logo.DOFade(1, 1));
        }
        #endregion // DOTween Animations

        #region UI Events
        /// <summary>
        /// Will fire when the start-button is clicked
        /// </summary>
        private void OnStartButtonClicked()
        {
            StartGame();
            UIController.instance.GoToGameView();
        }
        #endregion // UI Events

        // TODO: Move this to a GameController
        public void StartGame()
        {
            if (OnRestartGame != null)
            {
                OnRestartGame();
            }
        }

        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected override void StopScreen()
        {
            startbutton.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            logo.color = new Color(1, 1, 1, 0);
        }

        /// <summary>
        /// Unsubscribes to different events we used
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();
            startbutton.onClick.RemoveAllListeners();
            MenuManager.onOpeningSequenceEnded -= DoAnimation;
        }
    }
}