using System;
<<<<<<< HEAD
using UnityEngine;
using UnityEngine.UI;
using UI.Controllers;

namespace UI.Managers
{
    /// <summary>
    /// This class controlls the UI elements on the GameOver-Screen
    /// </summary>
    public class GameOverScreenManager : MonoBehaviour
    {
        public static GameOverScreenManager instance;

        [SerializeField] private Button resume;
        [SerializeField] private Button quit;
        [SerializeField] private Text score;

        [SerializeField] private GameObject gameOverScreen;

        /// <summary>
        /// Subscribes to alle the events we want to react on.
        /// </summary>
        private void OnEnable()
        {
            resume.onClick.AddListener(() => OnResumeButtonClicked());
            quit.onClick.AddListener(() => OnQuitButtonClicked());
        }

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

        #region UI Events
        /// <summary>
        /// Updates the score
        /// </summary>
        public void UpdateScore()
        {
            score.text = GameviewManager.instance.GetScore().ToString();
        }

        /// <summary>
        /// Will fire when the resume-button is clicked
        /// </summary>
        private void OnResumeButtonClicked()
        {
            gameOverScreen.SetActive(false);

            HomeScreenManager.instance.StartGame();
            GameviewManager.instance.ResetScore();
        }

        /// <summary>
        /// Will fire when the quit-button is clicked
        /// </summary>
        private void OnQuitButtonClicked()
        {
            gameOverScreen.SetActive(false);

            GameviewManager.instance.ResetScore();
            UIController.instance.GoToHomeScreen();
        }
        #endregion // UI Events

        /// <summary>
        /// Unubscribes to alle the events we used.
        /// </summary>
        private void OnDisable()
        {
            resume.onClick.RemoveAllListeners();
            quit.onClick.RemoveAllListeners();
        }
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class GameOverScreenManager : MonoBehaviour 
{
    public static GameOverScreenManager instance;

    [SerializeField] private Button resume;
   
    [SerializeField] private Button quit;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Image background;
    [SerializeField] private GameObject menu;
    [SerializeField] private Text score;

    public static Action onQuit;

    private void OnEnable()
    {
        resume.onClick.AddListener(() => OnResumeButtonClicked());
        quit.onClick.AddListener(() => OnQuitButtonClicked());
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    public void UpdateScore()
    {
        score.text = GameviewManager.instance.GetScore().ToString();
    }

    private void OnResumeButtonClicked()
    {
        HomeManager.instance.StartGame();
        gameOverScreen.SetActive(false);
        GameviewManager.instance.ResetScore();
    }

    private void OnQuitButtonClicked()
    {
        UIController.instance.GoToHomeScreen();
        gameOverScreen.SetActive(false);
        background.gameObject.SetActive(true);
        menu.SetActive(true);
        GameviewManager.instance.ResetScore();


        if (onQuit != null)
        {
            onQuit();
        }
    }
}
>>>>>>> Development_branch
