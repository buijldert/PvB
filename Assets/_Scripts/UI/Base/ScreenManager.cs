using UI.Controllers;
using UnityEngine;

namespace UI.Base
{
    public abstract class ScreenManager : MonoBehaviour
    {
        protected MenuState screenState;

        protected virtual void OnEnable()
        {
            UIController.OnScreenChanged += PrepareScreen;
        }

        /// <summary>
        /// Use this for the Singleton Implementation
        /// </summary>
        protected abstract void Awake();

        /// <summary>
        /// Checks if the screenState is the same as the MenuState
        /// </summary>
        /// <param name="state">The current state of the UI.</param>
        protected virtual void PrepareScreen(MenuState state)
        {
            if (state == screenState)
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

        protected virtual void OnDisable()
        {
            UIController.OnScreenChanged -= PrepareScreen;
        }
    }
}