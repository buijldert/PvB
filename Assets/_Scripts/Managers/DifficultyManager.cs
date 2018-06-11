using RR.Controllers;
using System;
using UnityEngine;

namespace RR.Managers
{
    /// <summary>
    /// This class is responsible for increasing the difficulty when the signal is given.
    /// </summary>
    public class DifficultyManager : MonoBehaviour
    {
        public static float GLOBAL_MOVEMENT_SPEED;

        public delegate void IncreaseDifficultyAction();
        public static IncreaseDifficultyAction IncreaseDifficulty;

        public static Action OnChangeDifficulty;

        private void OnEnable()
        {
            IncreaseDifficulty += IncreaseTheDifficulty;
            GameController.OnStartGame += ResetMovementSpeed;
        }

        private void OnDisable()
        {
            IncreaseDifficulty -= IncreaseTheDifficulty;
            GameController.OnStartGame -= ResetMovementSpeed;
        }

        /// <summary>
        /// Resets the global movement speed.
        /// </summary>
        private void ResetMovementSpeed()
        {
            GLOBAL_MOVEMENT_SPEED = 60f;
        }

        /// <summary>
        /// Sends out an event to increease the difficulty of the game.
        /// </summary>
        private void IncreaseTheDifficulty()
        {
            if(GLOBAL_MOVEMENT_SPEED < 100f)
            {
                GLOBAL_MOVEMENT_SPEED += 10f;
                if (OnChangeDifficulty != null)
                {
                    OnChangeDifficulty();
                }
            }
        }
    }
}