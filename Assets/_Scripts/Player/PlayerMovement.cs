using System.Collections;
using UI;
using UnityEngine;
using Utility;

namespace Player
{
    /// <summary>
    /// The PlayerColor enum is used to differentiate between the players alter egos.
    /// </summary>
    public enum PlayerColor
    {
        Black,
        White
    }

    /// <summary>
    /// This class is responsible for moving the player from right to left and the other way around with all the visuals that come with it.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerColor playerColor = PlayerColor.Black;

        private BoxCollider boxCollider;

        private bool canMove;

        private Coroutine _appearDelayCoroutine;

        [Header("Positions")]
        [SerializeField] private Vector3 leftPos;
        [SerializeField] private Vector3 rightPos;
        [SerializeField] private Vector3 particleLeftPos;
        [SerializeField] private Vector3 particleRightPos;
        [SerializeField] private Vector3 particleLeftRotation;
        [SerializeField] private Vector3 particleRightRotation;

        
        [Header("GameObjects")]
        [SerializeField] private GameObject particleSystemGameObject;
        [SerializeField] private GameObject blackPlayer, whitePlayer;
        
        public PlayerColor GetPlayerColor()
        {
            return playerColor;
        }

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += ResetPlayer;
            HomeManager.OnRestartGame += ResetPlayer;
            HomeManager.OnRestartGame += StartMovement;

            PlayerInput.OnLeftMouseButtonDown += TogglePlayer;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= ResetPlayer;
            HomeManager.OnRestartGame -= ResetPlayer;
            HomeManager.OnRestartGame -= StartMovement;
            PlayerInput.OnLeftMouseButtonDown -= TogglePlayer;

        }

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
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
                if (playerColor == PlayerColor.Black)
                {
                    ChangePlayer(leftPos, PlayerColor.White, particleRightPos, particleRightRotation);
                }
                else
                {
                    ChangePlayer(rightPos, PlayerColor.Black, particleLeftPos, particleLeftRotation);
                }
            }
        }

        /// <summary>
        /// Changes the player's position and color to the given values.
        /// </summary>
        /// <param name="_positionToMove">The position that the player will move to.</param>
        /// <param name="_playerColor">The PlayerColor that the player will be made.</param>
        /// <param name="_particlePosition">The position that the switch particle needs to be in.</param>
        /// <param name="_particleRotation">The rotation that the switch particle needs to be in.</param>
        /// <param name="_isReset">Whether the player is being reset to its normal position.</param>
        private void ChangePlayer(Vector3 _positionToMove, PlayerColor _playerColor, Vector3 _particlePosition, Vector3 _particleRotation, bool _isReset = false)
        {
            transform.position = _positionToMove;
            playerColor = _playerColor;

            canMove = false;
            if(_isReset)
            {
                blackPlayer.SetActive(true);
                whitePlayer.SetActive(false);
            }
            else
            {
                particleSystemGameObject.transform.position = _particlePosition;
                particleSystemGameObject.transform.rotation = Quaternion.Euler(_particleRotation);
                particleSystemGameObject.SetActive(true);

                blackPlayer.SetActive(false);
                whitePlayer.SetActive(false);

                boxCollider.enabled = false;
                if (_appearDelayCoroutine != null)
                {
                    StopCoroutine(_appearDelayCoroutine);
                }
                _appearDelayCoroutine = StartCoroutine(AppearDelay(_playerColor));
            }
        }

        /// <summary>
        /// Makes the player appear after a certain delay to make the visuals more interesting.
        /// </summary>
        /// <param name="_playerColor"></param>
        /// <returns></returns>
        private IEnumerator AppearDelay(PlayerColor _playerColor)
        {
            float particleSystemTime = 0.25f;
            yield return new WaitForSeconds(particleSystemTime);

            if (_playerColor == PlayerColor.White)
            {
                whitePlayer.SetActive(true);
            }
            else
            {
                blackPlayer.SetActive(true);
            }

            boxCollider.enabled = true;
            particleSystemGameObject.SetActive(false);
            canMove = true;
        }

        /// <summary>
        /// Resets the player to his original position and color.
        /// </summary>
        private void ResetPlayer()
        {
            ChangePlayer(rightPos, PlayerColor.Black, particleLeftPos, particleLeftRotation, true);
        }
    }
}