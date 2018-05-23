using UI;
using UnityEngine;
using Utility;

namespace Environment
{
    [RequireComponent(typeof(MeshRenderer))]
    public class RaceTrack : MonoBehaviour
    {
        private MeshRenderer trackRenderer;

        private Vector2 textureOffset;
        private float speed = 1f;

        private bool canMove;

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopTrack;
            RestartGameButton.OnRestartGame += StartMovement;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopTrack;
            RestartGameButton.OnRestartGame -= StartMovement;
        }

        private void Start()
        {
            trackRenderer = GetComponent<MeshRenderer>();
        }
        
        private void Update()
        {
            if(canMove)
            {
                ScrollTexture();
            }
        }

        /// <summary>
        /// Starts the scrolling movement of the scrolling texture.
        /// </summary>
        private void StartMovement()
        {
            canMove = true;
        }

        /// <summary>
        /// Scrolls the texture over the quad its attached to.
        /// </summary>
        private void ScrollTexture()
        {
            textureOffset = new Vector2(0, Time.time * speed);

            trackRenderer.material.mainTextureOffset = textureOffset;
        }

        private void StopTrack()
        {
            canMove = false;
        }
    }
}