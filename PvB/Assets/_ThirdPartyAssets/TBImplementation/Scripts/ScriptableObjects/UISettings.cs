using UnityEngine;
using UnityEngine.Events;

namespace TBImplementation.ScriptableObjects
{
    [CreateAssetMenu]
    public class UISettings : ScriptableObject
    {
        public UITheme Theme;

        /// <summary>
        /// Sets the UITheme.
        /// </summary>
        /// <param name="_UITheme">UITheme we want to use.</param>
        public void SetUITheme(UITheme _UITheme)
        {
            this.Theme = _UITheme;
        }

#if UNITY_EDITOR
        private UnityEvent onSettingsUpdated;

        /// <summary>
        /// In this function we create a new UnityEvent, onSettingsUpdated.
        /// </summary>
        public void OnEnable()
        {
            if (onSettingsUpdated == null)
            {
                onSettingsUpdated = new UnityEvent();
            }
        }

        /// <summary>
        /// Register a listener to the event.
        /// </summary>
        /// <param name="_call">The method we want to call.</param>
        public void RegisterListener(UnityAction _call)
        {
            onSettingsUpdated.AddListener(_call);
        }

        /// <summary>
        /// Checks if there were any changes to the editor window we are working in.
        /// If there are any changes we call onSettingsUpdated.
        /// </summary>
        private void OnValidate()
        {
            if (Theme != null && onSettingsUpdated != null)
            {
                onSettingsUpdated.Invoke();
            }
        }
#endif
    }
}