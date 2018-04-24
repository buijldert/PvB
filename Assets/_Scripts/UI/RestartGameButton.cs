using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class RestartGameButton : MonoBehaviour
    {
        public static Action OnRestartGame;

        [SerializeField] private Sprite _resetButtonSprite;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Button _restartButton;

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
            _restartButton.interactable = false;
            _buttonImage.enabled = false;
            _buttonImage.sprite = _resetButtonSprite;
                
            if (OnRestartGame != null)
                OnRestartGame();
        }

        /// <summary>
        /// Stops the game.
        /// </summary>
        private void StopGame()
        {
            _restartButton.interactable = true;
            _buttonImage.enabled = true;
        }
    }
}