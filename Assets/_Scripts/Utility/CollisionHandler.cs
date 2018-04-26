using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(Collider))]
    public class CollisionHandler : MonoBehaviour
    {
        public static Action OnDeadlyCollision;
        public delegate void FadeThroughCollision(float scoreMutation);
        public static FadeThroughCollision OnFadeThroughCollision;

        private PlayerMovement _playerMovement;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        /// <summary>
        /// Handles the collision of the player with the differenc obstacles.
        /// </summary>
        /// <param name="collision">The collider the player is colliding with.</param>
        private void OnTriggerEnter(Collider collision)
        {
            Debug.Log("collision");
            if ((collision.gameObject.tag == "WhiteObstacle" && _playerMovement.GetPlayerColor() == PlayerColor.Black) || 
                (collision.gameObject.tag == "BlackObstacle" && _playerMovement.GetPlayerColor() == PlayerColor.White))
            {
                Debug.Log("deadlycollision");
                if (OnDeadlyCollision != null)
                    OnDeadlyCollision();
            }
            else if(collision.gameObject.tag == "WhiteObstacle" || collision.gameObject.tag == "BlackObstacle")
            {
                if (OnFadeThroughCollision != null)
                    OnFadeThroughCollision(10f);
            }
        }
    }
}