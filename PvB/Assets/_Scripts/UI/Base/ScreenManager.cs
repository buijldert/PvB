using UnityEngine;
using UI.Controllers;
using TBImplementation.ScriptableObjects;

namespace UI.Base
{
    [ExecuteInEditMode]
    public abstract class ScreenManager : MonoBehaviour
    {
        protected MenuState screenState;

        /// <summary>
        /// This function subscribes to the various events that we need to use the UI system
        /// </summary>
        protected virtual void OnEnable()
        {
            UIController.OnScreenChanged += PrepareScreen;
            UIController.OnThemeChanged += OnThemeUpdated;
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
        /// Method that will execute when the OnThemeChanged event is fired
        /// </summary>
        /// <param name="_UITheme">The UITheme that will be active.</param>
        protected virtual void OnThemeUpdated(UITheme _UITheme)
        {
            if (_UITheme == null)
            {
                return;
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
        /// This function unsubscribs us from the events
        /// </summary>
        protected virtual void OnDisable()
        {
            UIController.OnScreenChanged -= PrepareScreen;
            UIController.OnThemeChanged -= OnThemeUpdated;
        }
    }
}