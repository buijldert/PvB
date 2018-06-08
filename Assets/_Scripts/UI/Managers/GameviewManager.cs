using UnityEngine;
using UnityEngine.UI;
using RR.UI.Base;
using RR.Handlers;
using RR.UI.Controllers;
using RR.Controllers;

namespace RR.UI.Managers 
{
    /// <summary>
    /// This class controlls als the UI that is used while playing the game, such as the pause functionality.
    /// </summary>
    public class GameviewManager : ScreenManager
    {
        public static GameviewManager instance;

        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject pauseScreen;

        [SerializeField] private Button pauseButton;
        [SerializeField] private Text scoreText;

        private int score;

        /// <summary>
        /// Subscribes to alle the events we want to react on.
        /// </summary>
        protected override void OnEnable()
        {
            CollisionHandler.OnFadeThroughCollision += UpdateScore;
            CollisionHandler.OnDeadlyCollision += ShowGameOver;

            pauseButton.onClick.AddListener(() => OnPauseButtonClicked());
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

            screenState = MenuState.GameView;
        }

        /// <summary>
        /// Will be called when we are on this particular screen
        /// </summary>
        protected override void StartScreen()
        {
            ResetScore();
        }

        #region UI Events
        /// <summary>
        /// Updates the score.
        /// </summary>
        /// <param name="_scoreMutation">Score mutation we want to apply.</param>
        private void UpdateScore(int _scoreMutation)
        {
            score += _scoreMutation;
            scoreText.text = score.ToString();
        }

        /// <summary>
        /// Resets the score.
        /// </summary>
        public void ResetScore()
        {
            pauseButton.interactable = true;
            score = 0;
            scoreText.text = score.ToString();
        }

        /// <summary>
        /// Shows the GameOver-screen
        /// </summary>
        private void ShowGameOver()
        {
            pauseButton.interactable = false;
            gameOverScreen.SetActive(true);
            GameOverScreenManager.instance.UpdateScore();
        }

        /// <summary>
        /// Will fire when the pause-button is clicked
        /// </summary>
        private void OnPauseButtonClicked()
        {
            pauseScreen.SetActive(true);
            GameController.instance.PauseGame();
        }
        #endregion // UI Events

        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected override void StopScreen()
        {
            pauseScreen.SetActive(false);
        }

        #region Getters & Setters
        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>The score.</returns>
        public int GetScore()
        {
            return score;
        }
        #endregion // Getters & Setters

        /// <summary>
        /// Unsubscribes to different events we used
        /// </summary>
        protected override void OnDisable()
        {
            CollisionHandler.OnFadeThroughCollision -= UpdateScore;
            CollisionHandler.OnDeadlyCollision -= ShowGameOver;

            pauseButton.onClick.RemoveAllListeners();
        }
    }
}