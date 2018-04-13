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

        private void ResetScore()
        {
            _score = 0;
            _scoreText.text = _score.ToString();
        }

        private void UpdateScore(float scoreMutation)
        {
            _score += scoreMutation;
            _scoreText.text = _score.ToString();
        }
    }
}