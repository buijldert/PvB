using System.Collections;
using UnityEngine;
using RR.Controllers;
using RR.Handlers;
using RR.Audio;
using RR.UI.Controllers;
using RR.UI.Managers;

namespace RR.Components.Player
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

        private PlayerColor playerColor = PlayerColor.Pink;

        private BoxCollider boxCollider;

        private Coroutine appearDelayCoroutine;

        private bool canMove;


        [Header("Audioclips")]
        [SerializeField] private AudioClip switchSoundEffect;

        /// <summary>
        /// Gets the player color.
        /// </summary>
        /// <returns></returns>
        public PlayerColor GetPlayerColor()
        {
            return playerColor;
        }

        /// <summary>
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += RemovePlayer;
            GameviewManager.OnLeftMouseButtonDown += TogglePlayer;
            GameController.OnStartGame += StartMovement;
            GameController.OnStopGame += ResetPlayerInstant;
        }

        /// <summary>
        /// OnDisable() is called before the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= RemovePlayer;
            GameviewManager.OnLeftMouseButtonDown -= TogglePlayer; 
            GameController.OnStartGame -= StartMovement;
            GameController.OnStopGame -= ResetPlayerInstant;
        }

        /// <summary>
        /// Start() is called after OnEnable() and Awake().
        /// </summary>
        private void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        
        /// <summary>
        /// Makes sure the player can start moving again after the level resets.
        /// </summary>
        private void StartMovement()
        {
            ResetPlayerInstant();
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
                SFXManager.instance.PlaySound(switchSoundEffect, _volume: 0.75f);
                for (int i = 0; i < switchParticleSystems.Length; i++)
                {
                    var main = switchParticleSystems[i].main;
                    main.startColor = _color;

                    switchParticleSystems[i].Clear();
                    switchParticleSystems[i].Play();
                }
                particleSystemGameObject.SetActive(true);

                pinkPlayer.SetActive(false);
                bluePlayer.SetActive(false);

                boxCollider.enabled = false;
                if (appearDelayCoroutine != null)
                {
                    StopCoroutine(appearDelayCoroutine);
                }
                appearDelayCoroutine = StartCoroutine(AppearDelay(_playerColor));
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
            canMove = true;

            particleSystemGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            yield return new WaitForSeconds(particleSystemTime * 2);
            particleSystemGameObject.SetActive(false);
        }

        /// <summary>
        /// Disables the player and makes sure he cant move.
        /// </summary>
        private void RemovePlayer()
        {
            bluePlayer.SetActive(false);
            pinkPlayer.SetActive(false);
            canMove = false;
        }

        /// <summary>
        /// Resets the player to his original position and color instantly.
        /// </summary>
        private void ResetPlayerInstant()
        {
            ChangePlayer(rightPos, PlayerColor.Pink, pinkColor, true);
        }
    }
}