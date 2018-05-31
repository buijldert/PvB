using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// This class is responsible for pausing and resuming the game at will.
    /// </summary>
    public class PauseGameManager : MonoBehaviour
    {
        public delegate void PauseGameAction();
        public static event PauseGameAction OnPauseGame;

        public delegate void ResumeGameAction();
        public static event ResumeGameAction OnResumeGame;

        [SerializeField] private GameObject pauseScreen;

        /// <summary>
        /// Pauses the game.
        /// </summary>
        public void PauseGame()
        {
            pauseScreen.SetActive(true);
            if (OnPauseGame != null)
            {
                OnPauseGame();
            }
            Time.timeScale = 0;
        }

        /// <summary>
        /// Resumes the game.
        /// </summary>
        public void ResumeGame()
        {
            Time.timeScale = 1;
            if (OnResumeGame != null)
            {
                OnResumeGame();
            }
            pauseScreen.SetActive(false);
        }
    }
}