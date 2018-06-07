using UnityEngine;
using RR.UI.Controllers;

namespace RR.UI.Base
{
    /// <summary>
    /// This class acts as a BaseClass for all major UI screens the game has
    /// </summary>
    public abstract class ScreenManager : MonoBehaviour
    {
        protected MenuState screenState;

        /// <summary>
        /// Subscribes to the events that every child class will use
        /// </summary>
        protected virtual void OnEnable()
        {
            UIController.OnScreenChanged += PrepareScreen;
        }

        /// <summary>
        /// Singleton Implementation
        /// </summary>
        protected abstract void Awake();

        /// <summary>
        /// Checks if the screenState is the same as the MenuState
        /// </summary>
        /// <param name="state">The current state of the UI.</param>
        protected virtual void PrepareScreen(MenuState _state)
        {
            if (_state == screenState)
            {
                StartScreen();
            }
            else
            {
                StopScreen();
            }
        }

        /// <summary>
        /// Will be called when we are on this particular screen
        /// </summary>
        protected abstract void StartScreen();

        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected abstract void StopScreen();

        /// <summary>
        /// Unsubscribes to the events that every child class used
        /// </summary>
        protected virtual void OnDisable()
        {
            UIController.OnScreenChanged -= PrepareScreen;
        }
    }
}