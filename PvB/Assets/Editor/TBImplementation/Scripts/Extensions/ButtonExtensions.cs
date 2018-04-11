using UnityEngine;
using UnityEngine.UI;
using TBImplementation.Models;

namespace TBImplementation.Extensions
{
    public static class ButtonExtensions
    {
        /// <summary>
        /// An extensionmethod to make the code cleaner. It will update position and dimensions when called.
        /// </summary>
        /// <param name="button">The Button we want to change.</param>
        /// <param name="model">The UIButtonModel which we use to get the data we want to change.</param>
        public static void UpdateButtonTheme(this Button button, UIButtonModel model)
        {
            RectTransform transform = button.gameObject.transform as RectTransform;
            Vector2 position = transform.anchoredPosition;

            if (model.ButtonSprite != null)
            {
                transform.sizeDelta = new Vector2(model.Width, model.Height);
                button.GetComponent<Image>().sprite = model.ButtonSprite;
            }

            transform.anchoredPosition = model.Position;
        }
    }
}