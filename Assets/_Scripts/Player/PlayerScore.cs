using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Player
{
    public class PlayerScore : MonoBehaviour
    {
        private float score;
        
        [SerializeField]private Text scoreText;

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
            score = 0;
            scoreText.text = score.ToString();
        }

        /// <summary>
        /// Updates the score by the given scoreMutation
        /// </summary>
        /// <param name="_scoreMutation">The given scoreMutation.</param>
        private void UpdateScore(float _scoreMutation)
        {
            score += _scoreMutation;
            scoreText.text = score.ToString();
        }
    }
}