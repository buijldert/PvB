using UI;
using UI.Managers;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Player
{
    /// <summary>
    /// This class is respnsible for keeping of -and displaying the score that the player has scored.
    /// </summary>
    public class PlayerScore : MonoBehaviour
    {
        private int score;
        
        [SerializeField]private Text scoreText;

        private void OnEnable()
        {
            CollisionHandler.OnFadeThroughCollision += UpdateScore;
        }

        private void OnDisable()
        {
            CollisionHandler.OnFadeThroughCollision -= UpdateScore;
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
        private void UpdateScore(int _scoreMutation)
        {
            score += _scoreMutation;
            scoreText.text = score.ToString();
        }
    }
}