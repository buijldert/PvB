using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Rigidbody))]
    public class ObstacleMovement : MonoBehaviour
    {
        private Rigidbody _rb;
        private float _movementSpeed = 300f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        private void FixedUpdate()
        {
            MoveObstacle();
        }

        /// <summary>
        /// Moves the obstacle towards the player.
        /// </summary>
        private void MoveObstacle()
        {
            _rb.velocity = Vector3.back * _movementSpeed;
        }
    }
}