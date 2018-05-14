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
        public delegate void FadeThroughCollision(int _scoreMutation);
        public static FadeThroughCollision OnFadeThroughCollision;

        private PlayerMovement playerMovement;

        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        /// <summary>
        /// Handles the collision of the player with the differenc obstacles.
        /// </summary>
        /// <param name="_collision">The collider the player is colliding with.</param>
        private void OnTriggerEnter(Collider _collision)
        {
            if ((_collision.gameObject.tag == "WhiteObstacle" && playerMovement.GetPlayerColor() == PlayerColor.Pink) || 
                (_collision.gameObject.tag == "BlackObstacle" && playerMovement.GetPlayerColor() == PlayerColor.Blue))
            {
                if (OnDeadlyCollision != null)
                    OnDeadlyCollision();
            }
            else if(_collision.gameObject.tag == "WhiteObstacle" || _collision.gameObject.tag == "BlackObstacle")
            {
                if (OnFadeThroughCollision != null)
                    OnFadeThroughCollision(10);
            }
        }
    }
}