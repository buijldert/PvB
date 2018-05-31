using System;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    /// <summary>
    /// This class is responsible (re)starting and stopping the game.
    /// </summary>
    public class RestartGameButton : MonoBehaviour
    {
        //public static Action OnRestartGame;
        
        [SerializeField] private Sprite resetButtonSprite;
        [SerializeField] private Image buttonImage;
        [SerializeField] private Button restartButton;

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopGame;

        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopGame;

        }

        /// <summary>
        /// (Re)starts the game.
        /// </summary>
        public void RestartGame()
        {
            restartButton.interactable = false;
            buttonImage.enabled = false;
            buttonImage.sprite = resetButtonSprite;

            //if (OnRestartGame != null)
            //{
            //    OnRestartGame();
            //}
        }

        /// <summary>
        /// Stops the game.
        /// </summary>
        private void StopGame()
        {
            restartButton.interactable = true;
            buttonImage.enabled = true;
        }
    }
}