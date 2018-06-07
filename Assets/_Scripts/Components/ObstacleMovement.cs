using UnityEngine;
using RR.Managers;

namespace RR.Components
{
    /// <summary>
    /// This class is responsible for moving the obstacle backwards at a certain movment speed.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ObstacleMovement : MonoBehaviour
    {
        private Rigidbody rigidBody;
        private float movementSpeed = 60f;

        private void SetMovementSpeed(float _movementSpeedIncrease)
        {
            movementSpeed += _movementSpeedIncrease;
        }

        private void OnEnable()
        {
            DifficultyManager.OnChangeDifficulty += SetMovementSpeed;
        }

        private void OnDisable()
        {
            DifficultyManager.OnChangeDifficulty -= SetMovementSpeed;
        }

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }
        
        private void Update()
        {
            MoveObstacle();
        }

        /// <summary>
        /// Moves the obstacle backwards.
        /// </summary>
        private void MoveObstacle()
        {
            rigidBody.velocity = Vector3.back * movementSpeed;
        }
    }
}