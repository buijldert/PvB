using UnityEngine;
using RR.Managers;

namespace RR.Components
{
    /// <summary>
    /// This class is responsible for moving the obstacle backwards at a certain movment speed.
    /// </summary>
    public class ObstacleMovement : MonoBehaviour
    {
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
        
        private void Update()
        {
            MoveObstacle();
        }

        /// <summary>
        /// Moves the obstacle backwards.
        /// </summary>
        private void MoveObstacle()
        {
            transform.position -= new Vector3(0, 0, movementSpeed * Time.deltaTime);
        }
    }
}