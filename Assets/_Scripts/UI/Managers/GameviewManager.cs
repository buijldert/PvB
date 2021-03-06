﻿using UnityEngine;
using UnityEngine.UI;
using RR.UI.Base;
using RR.Handlers;
using RR.UI.Controllers;
using RR.Controllers;
using System;

namespace RR.UI.Managers 
{
    /// <summary>
    /// This class controlls als the UI that is used while playing the game, such as the pause functionality.
    /// </summary>
    public class GameviewManager : ScreenManager
    {
        public static GameviewManager instance;

        public static Action OnLeftMouseButtonDown;

        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject pauseScreen;

        [SerializeField] private Button pauseButton;
        [SerializeField] private Button switchAreaButton;
        [SerializeField] private Text scoreText;

        private int score;

        /// <summary>
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        protected override void OnEnable()
        {
            CollisionHandler.OnFadeThroughCollision += UpdateScore;
            CollisionHandler.OnDeadlyCollision += ShowGameOver;

            pauseButton.onClick.AddListener(() => OnPauseButtonClicked());
            switchAreaButton.onClick.AddListener(() => SwitchPlayer());
        }

        /// <summary>
        /// Awake() is called before Start() and OnEnable().
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
            SetButtonInteractable(true);
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
            SetButtonInteractable(false);
        }

        /// <summary>
        /// Will fire when the pause-button is clicked
        /// </summary>
        private void OnPauseButtonClicked()
        {
            pauseScreen.SetActive(true);
            GameController.instance.PauseGame();
            SetButtonInteractable(false);
        }

        /// <summary>
        /// Handles the input from the player by sending out an Action when input is given.
        /// </summary>
        private void SwitchPlayer()
        {
            if (OnLeftMouseButtonDown != null)
            {
                OnLeftMouseButtonDown();
            }
        }

        public void SetButtonInteractable(bool _value)
        {
            switchAreaButton.interactable = _value;
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
        /// OnDisable() is called before the object is disabled.
        /// </summary>
        protected override void OnDisable()
        {
            CollisionHandler.OnFadeThroughCollision -= UpdateScore;
            CollisionHandler.OnDeadlyCollision -= ShowGameOver;

            pauseButton.onClick.RemoveAllListeners();
            switchAreaButton.onClick.RemoveAllListeners();
        }
    }
}