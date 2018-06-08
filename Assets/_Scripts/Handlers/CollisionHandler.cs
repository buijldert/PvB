using System;
using UnityEngine;
using RR.Components.Player;
using RR.Controllers;
using RR.Components;

namespace RR.Handlers
{
    /// <summary>
    /// This class is responsible for handling the collision between the player and the gates.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private GameObject fadeParticleGameObject;

        private const string WHITE_OBSTACLE_TAG = "WhiteObstacle";
        private const string BLACK_OBSTACLE_TAG = "BlackObstacle";

        public static Action<int> OnFadeThroughCollision;
        public static Action OnDeadlyCollision;

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
            if ((_collision.gameObject.tag == WHITE_OBSTACLE_TAG && playerMovement.GetPlayerColor() == PlayerColor.Pink) || 
                (_collision.gameObject.tag == BLACK_OBSTACLE_TAG && playerMovement.GetPlayerColor() == PlayerColor.Blue))
            {
                if (OnDeadlyCollision != null)
                {
                    OnDeadlyCollision();

                    //TODO: put this somewhere else..
                    if(SettingsController.GetVibrationState())
                    {
                        Handheld.Vibrate();
                    }
                }
                    
            }
            else if(_collision.gameObject.tag == WHITE_OBSTACLE_TAG || _collision.gameObject.tag == BLACK_OBSTACLE_TAG)
            {
                if (OnFadeThroughCollision != null)
                {
                    GameObject fadeParticleClone = ObjectPool.instance.GetObjectForType(fadeParticleGameObject.name, false);
                    fadeParticleClone.transform.position = _collision.transform.position;
                    //foreach(ParticleSystem p in fadeParticleClone.GetComponentInChildren<ParticleSystem>())
                    OnFadeThroughCollision(10);
                }
            }
        }
    }
}