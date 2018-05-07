using DG.Tweening;
using UI;
using UnityEngine;
using Utility;

namespace Player
{
    public enum PlayerColor
    {
        Pink,
        Blue
    }

    [RequireComponent(typeof(MeshRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerColor playerColor = PlayerColor.Pink;

        private MeshRenderer mr;

        [SerializeField] private Vector3 leftPos, rightPos;

        [SerializeField] private Color pink, blue;

        private bool canMove;

        public PlayerColor GetPlayerColor()
        {
            return playerColor;
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
            mr = GetComponent<MeshRenderer>();
            
        }
        
        /// <summary>
        /// Makes sure the player can start moving again after the level resets.
        /// </summary>
        private void StartMovement()
        {
            canMove = true;
        }

        /// <summary>
        /// Toggles the player's position and color.
        /// </summary>
        private void TogglePlayer()
        {
            if (canMove)
            {
                if (playerColor == PlayerColor.Pink)
                {
                    ChangePlayer(leftPos, blue, PlayerColor.Blue);
                }
                else
                {
                    ChangePlayer(rightPos, pink, PlayerColor.Pink);
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
            mr.material.DOColor(colorToMake, 0f);
            playerColor = color;
        }

        /// <summary>
        /// Resets the player to his original position and color.
        /// </summary>
        private void ResetPlayer()
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            canMove = false;
            ChangePlayer(rightPos, pink, PlayerColor.Pink);
        }
    }
}