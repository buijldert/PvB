using System;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// This class is responsible for increasing the difficulty when the signal is given.
    /// </summary>
    public class DifficultyManager : MonoBehaviour
    {
        public delegate void IncreaseDifficultyAction();
        public static IncreaseDifficultyAction IncreaseDifficulty;

        public static Action<float> OnChangeDifficulty;

        private void OnEnable()
        {
            IncreaseDifficulty += ChangeDifficulty;
        }

        private void OnDisable()
        {
            IncreaseDifficulty -= ChangeDifficulty;
        }

        /// <summary>
        /// Sends out an event to increease the difficulty of the game.
        /// </summary>
        private void ChangeDifficulty()
        {
            if(OnChangeDifficulty != null)
            {
                OnChangeDifficulty(10f);
            }
        }
    }
}