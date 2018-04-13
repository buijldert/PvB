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
        private PlayerColor _playerColor;

        private MeshRenderer _mr;

        [SerializeField] private Vector3 _leftPos, _rightPos;

        private bool _canMove;

        public PlayerColor GetPlayerColor()
        {
            return _playerColor;
        }

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += ResetPlayer;
            RestartGameButton.OnRestartGame += StartMovment;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= ResetPlayer;
            RestartGameButton.OnRestartGame -= StartMovment;
        }

        private void Start()
        {
            _mr = GetComponent<MeshRenderer>();
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _canMove)
            {
                if (gameObject.layer == 11)
                {
                    ChangePlayer(10, _leftPos, Color.white, PlayerColor.White);
                }
                else
                {
                    ChangePlayer(11, _rightPos, Color.black, PlayerColor.Black);
                }
            }
        }
        
        /// <summary>
        /// Makes sure the player can start moving again after the level resets.
        /// </summary>
        private void StartMovment()
        {
            _canMove = true;
        }

        /// <summary>
        /// Changes the player's layer, position and color to the given values.
        /// </summary>
        /// <param name="layer">The (physics) layer that the player will be placed on.</param>
        /// <param name="positionToMove">The position that the player will move to.</param>
        /// <param name="colorToMake">The color that the player will be made.</param>
        private void ChangePlayer(int layer, Vector3 positionToMove, Color colorToMake, PlayerColor color)
        {
            gameObject.layer = layer;
            transform.position = positionToMove;
            _mr.material.DOColor(colorToMake, 0f);
            _playerColor = color;
        }

        /// <summary>
        /// Resets the player to his original layer, position and color.
        /// </summary>
        private void ResetPlayer()
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _canMove = false;
            ChangePlayer(11, _rightPos, Color.black, PlayerColor.Black);
        }
    }
}