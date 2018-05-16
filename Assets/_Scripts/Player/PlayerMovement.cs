using DG.Tweening;
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
        

        [SerializeField] private Vector3 leftPos, rightPos;

        [SerializeField] private PlayerModel blackPlayerModel, whitePlayerModel;

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
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();
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
                    ChangePlayer(leftPos, PlayerColor.White, whitePlayerModel);
                }
                else
                {
                    ChangePlayer(rightPos, PlayerColor.Black, blackPlayerModel);
                }
            }
        }

        /// <summary>
        /// Changes the player's position and color to the given values.
        /// </summary>
        /// <param name="positionToMove">The position that the player will move to.</param>
        /// <param name="colorToMake">The color that the player will be made.</param>
        /// <param name="color">The PlayerColor that the player will be made.</param>
        private void ChangePlayer(Vector3 positionToMove,PlayerColor color, PlayerModel model)
        {
            transform.DOMove(positionToMove, 0f);
            playerColor = color;
            meshFilter.mesh = model.PlayerMeshFilter.sharedMesh;
            meshRenderer.material = model.PlayerMeshRenderer.sharedMaterial;
        }

        /// <summary>
        /// Resets the player to his original position and color.
        /// </summary>
        private void ResetPlayer()
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            canMove = false;
            ChangePlayer(rightPos, PlayerColor.Black, blackPlayerModel);
        }
    }
}