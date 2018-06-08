using System;
using UnityEngine;
using UnityEngine.UI;

namespace RR.Components.Player
{
    /// <summary>
    /// This class is responsible for sending out a message when the switch area is clicked.
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Button switchAreaButton;

        public static Action OnLeftMouseButtonDown;

        private void OnEnable()
        {
            switchAreaButton.onClick.AddListener(() => SwitchPlayer());
        }

        private void OnDestroy()
        {
            switchAreaButton.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// Handles the input from the player by sending out an Action when input is given.
        /// </summary>
        private void SwitchPlayer()
        {
            if (OnLeftMouseButtonDown != null)
            {
                OnLeftMouseButtonDown();
            }
        }
    }
}