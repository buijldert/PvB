using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
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

        private void StartGame()
        {
            if(OnStartGame != null)
            {
                OnStartGame();
            }
        }

        private void StopGame()
        {
            Time.timeScale = 1;
            if (OnStopGame != null)
            {
                OnStopGame();
            } 
        }

        private void PauseGame()
        {
            if(OnPauseGame != null)
            {
                OnPauseGame();
            }
            Time.timeScale = 0;
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
            if (OnResumeGame != null)
            {
                OnResumeGame();
            }
        }
    }
}