using DG.Tweening;
using System.Collections;
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
        private PlayerColor playerColor = PlayerColor.Black;

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private BoxCollider boxCollider;

        [SerializeField] private Vector3 leftPos, rightPos;
        [SerializeField] private Vector3 particleLeftPos, particleRightPos;
        [SerializeField] private Vector3 particleLeftRotation, particleRightRotation;

        [SerializeField] private PlayerModel blackPlayerModel, whitePlayerModel;

        private bool canMove;

        private Coroutine _appearDelayCoroutine;

        [SerializeField] private GameObject particleSystemGO;

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
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();
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
                    ChangePlayer(leftPos, PlayerColor.White, whitePlayerModel, particleRightPos, particleRightRotation);
                }
                else
                {
                    ChangePlayer(rightPos, PlayerColor.Black, blackPlayerModel, particleLeftPos, particleLeftRotation);
                }
            }
        }

        /// <summary>
        /// Changes the player's position and color to the given values.
        /// </summary>
        /// <param name="_positionToMove">The position that the player will move to.</param>
        /// <param name="_color">The PlayerColor that the player will be made.</param>
        /// <param name="_particlePosition">The position that the switch particle needs to be in.</param>
        /// <param name="_particleRotation">The rotation that the switch particle needs to be in.</param>
        /// <param name="_isReset">Whether the player is being reset to its normal position.</param>
        private void ChangePlayer(Vector3 _positionToMove, PlayerColor _color, PlayerModel _model, Vector3 _particlePosition, Vector3 _particleRotation, bool _isReset = false)
        {
            transform.position = _positionToMove;
            playerColor = _color;
            meshFilter.mesh = _model.PlayerMeshFilter.sharedMesh;
            meshRenderer.material = _model.PlayerMeshRenderer.sharedMaterial;

            canMove = false;
            if(!_isReset)
            {
                particleSystemGO.transform.position = _particlePosition;
                particleSystemGO.transform.rotation = Quaternion.Euler(_particleRotation);
                particleSystemGO.SetActive(true);
                boxCollider.enabled = false;
                meshRenderer.enabled = false;
                if (_appearDelayCoroutine != null)
                    StopCoroutine(_appearDelayCoroutine);
                _appearDelayCoroutine = StartCoroutine(AppearDelay());
            }
        }

        private IEnumerator AppearDelay()
        {
            float particleSystemTime = 0.25f;
            yield return new WaitForSeconds(particleSystemTime);
            meshRenderer.enabled = true;
            boxCollider.enabled = true;
            particleSystemGO.SetActive(false);
            canMove = true;
        }

        /// <summary>
        /// Resets the player to his original position and color.
        /// </summary>
        private void ResetPlayer()
        {
            ChangePlayer(rightPos, PlayerColor.Black, blackPlayerModel, particleLeftPos, particleLeftRotation, true);
        }
    }
}