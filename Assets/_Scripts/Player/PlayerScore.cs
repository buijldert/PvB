using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Player
{
    public class PlayerScore : MonoBehaviour
    {
        private float _score;

        [SerializeField]private Text _scoreText;

        private void OnEnable()
        {
            CollisionHandler.OnFadeThroughCollision += UpdateScore;
            RestartGameButton.OnRestartGame += ResetScore;
        }

        private void OnDisable()
        {
            CollisionHandler.OnFadeThroughCollision -= UpdateScore;
            RestartGameButton.OnRestartGame -= ResetScore;
        }

        /// <summary>
        /// Resets the score to 0.
        /// </summary>
        private void ResetScore()
        {
            _score = 0;
            _scoreText.text = _score.ToString();
        }

        /// <summary>
        /// Updates the score by the given scoreMutation
        /// </summary>
        /// <param name="scoreMutation">The given scoreMutation.</param>
        private void UpdateScore(float scoreMutation)
        {
            _score += scoreMutation;
            _scoreText.text = _score.ToString();
        }
    }
}