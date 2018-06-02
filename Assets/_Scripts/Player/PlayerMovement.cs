using System.Collections;
using UI;
using UI.Managers;
using UnityEngine;
using Utility;

namespace Player
{
    /// <summary>
    /// The PlayerColor enum is used to differentiate between the players alter egos.
    /// </summary>
    public enum PlayerColor
    {
        Pink,
        Blue
    }

    /// <summary>
    /// This class is responsible for moving the player from right to left and the other way around with all the visuals that come with it.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerColor playerColor = PlayerColor.Pink;

        private BoxCollider boxCollider;

        private bool canMove;

        private Coroutine _appearDelayCoroutine;

        [Header("Positions")]
        [SerializeField] private Vector3 leftPos;
        [SerializeField] private Vector3 rightPos;

        
        [Header("GameObjects")]
        [SerializeField] private GameObject particleSystemGameObject;
        [SerializeField] private GameObject pinkPlayer;
        [SerializeField] private GameObject bluePlayer;

        [Header("Particle Systems")]
        [SerializeField] private ParticleSystem[] switchParticleSystems;

        [Header("Colors")]
        [SerializeField] private Color pinkColor;
        [SerializeField] private Color blueColor;
        
        public PlayerColor GetPlayerColor()
        {
            return playerColor;
        }

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += ResetPlayer;
            HomeScreenManager.OnRestartGame += ResetPlayer;
            HomeScreenManager.OnRestartGame += StartMovement;

            PlayerInput.OnLeftMouseButtonDown += TogglePlayer;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= ResetPlayer;
            HomeScreenManager.OnRestartGame -= ResetPlayer;
            HomeScreenManager.OnRestartGame -= StartMovement;
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
                if (playerColor == PlayerColor.Pink)
                {
                    ChangePlayer(leftPos, PlayerColor.Blue, blueColor);
                }
                else
                {
                    ChangePlayer(rightPos, PlayerColor.Pink, pinkColor);
                }
            }
        }

        /// <summary>
        /// Changes the player's position and color to the given values.
        /// </summary>
        /// <param name="_positionToMove">The position that the player will move to.</param>
        /// <param name="_playerColor">The PlayerColor that the player will be made.</param>
        /// <param name="_isReset">Whether the player is being reset to its normal position.</param>
        private void ChangePlayer(Vector3 _positionToMove, PlayerColor _playerColor, Color _color, bool _isReset = false)
        {
            transform.position = _positionToMove;
            playerColor = _playerColor;

            canMove = false;
            if(_isReset)
            {
                pinkPlayer.SetActive(true);
                bluePlayer.SetActive(false);
            }
            else
            {
                particleSystemGameObject.SetActive(true);

                for (int i = 0; i < switchParticleSystems.Length; i++)
                {
                    var main = switchParticleSystems[i].main;
                    main.startColor = _color;
                }

                pinkPlayer.SetActive(false);
                bluePlayer.SetActive(false);

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

            if (_playerColor == PlayerColor.Blue)
            {
                bluePlayer.SetActive(true);
            }
            else
            {
                pinkPlayer.SetActive(true);
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
            ChangePlayer(rightPos, PlayerColor.Pink, pinkColor, true);
        }
    }
}