using System;
using UnityEngine;

namespace RR.Components.Player
{
    /// <summary>
    /// This class is responsible for sending out a message when certain inputs are given.
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        public static Action OnLeftMouseButtonDown;

        private float mouseMaxY = 800f;

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
                if (OnLeftMouseButtonDown != null && Input.mousePosition.y < mouseMaxY)
                {
                    OnLeftMouseButtonDown();
                }
            }
        }
    }
}