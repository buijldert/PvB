using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Rigidbody))]
    public class ObstacleMovement : MonoBehaviour
    {
        private Rigidbody rb;
        private float movementSpeed = 75f;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
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
            rb.velocity = Vector3.back * movementSpeed;
        }
    }
}