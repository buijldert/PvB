using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utility;

namespace Player
{
    public enum PlayerColor
    {
        Black,
        White
    }

    [RequireComponent(typeof(MeshRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerColor _playerColor = PlayerColor.Black;

        private MeshRenderer _mr;

        [SerializeField] private Vector3 _leftPos, _rightPos;

        [SerializeField] private Color _pink, _blue;

        private bool _canMove;

        public PlayerColor GetPlayerColor()
        {
            return _playerColor;
        }

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += ResetPlayer;
            RestartGameButton.OnRestartGame += StartMovement;
            PlayerInput.OnLeftMouseButtonDown += TogglePlayer;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= ResetPlayer;
            RestartGameButton.OnRestartGame -= StartMovement;
            PlayerInput.OnLeftMouseButtonDown -= TogglePlayer;
        }

        private void Start()
        {
            _mr = GetComponent<MeshRenderer>();
            
        }
        
        /// <summary>
        /// Makes sure the player can start moving again after the level resets.
        /// </summary>
        private void StartMovement()
        {
            _canMove = true;
        }

        /// <summary>
        /// Toggles the player's position and color.
        /// </summary>
        private void TogglePlayer()
        {
            if (_canMove)
            {
                if (_playerColor == PlayerColor.Black)
                {
                    ChangePlayer(_leftPos, _blue, PlayerColor.White);
                }
                else
                {
                    ChangePlayer(_rightPos, _pink, PlayerColor.Black);
                }
            }
        }

        /// <summary>
        /// Changes the player's position and color to the given values.
        /// </summary>
        /// <param name="positionToMove">The position that the player will move to.</param>
        /// <param name="colorToMake">The color that the player will be made.</param>
        /// <param name="color">The PlayerColor that the player will be made.</param>
        private void ChangePlayer(Vector3 positionToMove, Color colorToMake, PlayerColor color)
        {
            transform.DOMove(positionToMove, 0f);
            _mr.material.DOColor(colorToMake, 0f);
            _playerColor = color;
        }

        /// <summary>
        /// Resets the player to his original position and color.
        /// </summary>
        private void ResetPlayer()
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _canMove = false;
            ChangePlayer(_rightPos, _pink, PlayerColor.Black);
        }
    }
}