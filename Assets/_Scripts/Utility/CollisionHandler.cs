using Player;
using System;
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
        /// Handles the collision of the player with the different obstacles.
        /// </summary>
        /// <param name="_collision">The collider the player is colliding with.</param>
        private void OnTriggerEnter(Collider _collision)
        {
            print("collision");
            if ((_collision.gameObject.tag == "WhiteObstacle" && playerMovement.GetPlayerColor() == PlayerColor.Black) || 
                (_collision.gameObject.tag == "BlackObstacle" && playerMovement.GetPlayerColor() == PlayerColor.White))
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