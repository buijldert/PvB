using System;
using UnityEngine;

namespace RR.Controllers
{
    /// <summary>
    /// This class is responsible for starting/stopping/pausing/resuming the game.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        public static Action OnStartGame;
        public static Action OnStopGame;
        public static Action OnPauseGame;
        public static Action OnResumeGame;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            instance = this;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            if(OnStartGame != null)
            {
                OnStartGame();
            }
        }

        /// <summary>
        /// Stops the game.
        /// </summary>
        public void StopGame()
        {
            Time.timeScale = 1;
            if (OnStopGame != null)
            {
                OnStopGame();
            } 
        }

        /// <summary>
        /// Pasues the game.
        /// </summary>
        public void PauseGame()
        {
            if(OnPauseGame != null)
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
        }
    }
}