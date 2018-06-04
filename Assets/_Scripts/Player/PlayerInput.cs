using System;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// This class is responsible for sending out a message when certain inputs are given.
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        public static Action OnLeftMouseButtonDown;

        private void Update()
        {
            Inputs();
        }

        /// <summary>
        /// Handles the input from the player by sending out an Action when input is given.
        /// </summary>
        private void Inputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Input");

                if (OnLeftMouseButtonDown != null)
                {
                    OnLeftMouseButtonDown();
                }
            }
        }
    }
}