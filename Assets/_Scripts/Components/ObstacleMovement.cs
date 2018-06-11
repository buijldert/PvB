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

        /// <summary>
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        private void OnEnable()
        {
            UpdateMovementSpeed();
            DifficultyManager.OnChangeDifficulty += UpdateMovementSpeed;
        }

        /// <summary>
        /// OnDisable() is called before the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            DifficultyManager.OnChangeDifficulty -= UpdateMovementSpeed;
        }

        /// <summary>
        /// Update() is called once per frame.
        /// </summary>
        private void Update()
        {
            MoveObstacle();
        }

        /// <summary>
        /// Updates the movement speed to increase the difficulty somewhat.
        /// </summary>
        private void UpdateMovementSpeed()
        {
            movementSpeed = DifficultyManager.GLOBAL_MOVEMENT_SPEED;
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