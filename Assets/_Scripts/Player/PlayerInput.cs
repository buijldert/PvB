using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
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
                if (OnLeftMouseButtonDown != null)
                    OnLeftMouseButtonDown();
            }
        }
    }
}