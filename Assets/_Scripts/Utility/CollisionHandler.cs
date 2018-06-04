﻿using Player;
using System;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// This class is responsible for handling the collision between the player and the gates.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class CollisionHandler : MonoBehaviour
    {
        public static Action OnDeadlyCollision;
        public static Action<int> OnFadeThroughCollision;

        private PlayerMovement playerMovement;

        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        /// <summary>
        /// Handles the collision of the player with the different obstacles.
        /// </summary>
        /// <param name="_collision">The collider the player is colliding with.</param>
        private void OnTriggerEnter(Collider _collision)
        {
            if ((_collision.gameObject.tag == "WhiteObstacle" && playerMovement.GetPlayerColor() == PlayerColor.Pink) || 
                (_collision.gameObject.tag == "BlackObstacle" && playerMovement.GetPlayerColor() == PlayerColor.Blue))
            {
                if (OnDeadlyCollision != null)
                {
                    OnDeadlyCollision();

                    if(SettingsController.GetVibrationState())
                    {
                        Handheld.Vibrate();
                    }
                }
                    
            }
            else if(_collision.gameObject.tag == "WhiteObstacle" || _collision.gameObject.tag == "BlackObstacle")
            {
                if (OnFadeThroughCollision != null)
                {
                    OnFadeThroughCollision(10);
                }
            }
        }
    }
}