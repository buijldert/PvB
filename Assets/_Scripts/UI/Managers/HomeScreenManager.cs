using UnityEngine;
using UnityEngine.UI;
using RR.UI.Base;
using RR.UI.Controllers;
using RR.Controllers;
using DG.Tweening;

namespace RR.UI.Managers
{
    /// <summary>
    /// This class controlls the UI Elements on the Home-Screen
    /// </summary>
    public class HomeScreenManager : ScreenManager
    {
        public static HomeScreenManager instance;

        [SerializeField] private Button startbutton;
        [SerializeField] private Image logo;
        [SerializeField] private Text highScore;

        private Sequence startUpSequence;
        private bool isFirstStarup = true;

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
            if(PlayerPrefs.HasKey("HighScore"))
            {
                highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
            }
            else
            {
                highScore.text = string.Empty;
            }

            if(!isFirstStarup)
            {
                DoAnimation();
            }
        }

        #region DOTween Animations
        /// <summary>
        /// Fades in the Logo and start-button
        /// </summary>
        public void DoAnimation()
        {
            startUpSequence = DOTween.Sequence();
            startUpSequence.Append(startbutton.GetComponent<Image>().DOFade(1, 1));
            startUpSequence.Join(logo.DOFade(1, 1));
            startUpSequence.Join(highScore.DOFade(1,1));

            isFirstStarup = false;
        }
        #endregion // DOTween Animations

        #region UI Events
        /// <summary>
        /// Will fire when the start-button is clicked
        /// </summary>
        private void OnStartButtonClicked()
        {
            UIController.instance.GoToGameView();
            GameController.instance.StartGame();
        }
        #endregion // UI Events

        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected override void StopScreen()
        {
            startbutton.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            logo.color = new Color(1, 1, 1, 0);
            highScore.color = new Color(1, 1, 1, 0);

            startUpSequence.Kill();
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