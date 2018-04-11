using UnityEngine;
using UnityEngine.Events;
using TBImplementation.Models;

namespace TBImplementation.ScriptableObjects
{
    [CreateAssetMenu]
    public class UITheme : ScriptableObject
    {
        public Sprite icon;

        [Header("Menu")]
        public UIButtonModel ButtonPlay;
        public UIButtonModel ButtonShop;
        public UIButtonModel ButtonSettings;
        public UIButtonModel ButtonMusic;

        [Header("Shared Elements")]
        public UIButtonModel BackButton;

#if UNITY_EDITOR
        private UnityEvent onThemeUpdated;

        /// <summary>
        /// In this function we create a new UnityEvent, onThemeUpdated.
        /// </summary>
        public void OnEnable()
        {
            if (onThemeUpdated == null)
            {
                onThemeUpdated = new UnityEvent();
            }
        }

        /// <summary>
        /// Register a listener to the event.
        /// </summary>
        /// <param name="_call">The method we want to call.</param>
        public void RegisterListener(UnityAction _call)
        {
            onThemeUpdated.AddListener(_call);
        }

        /// <summary>
        /// Checks if there were any changes to the editor window we are working in.
        /// If there are any changes we call onThemeUpdated.
        /// </summary>
        private void OnValidate()
        {
            if (onThemeUpdated != null)
            {
                onThemeUpdated.Invoke();
            }
        }
#endif
    }
}