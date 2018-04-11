using UnityEditor;
using UnityEngine;
using TBImplementation.Models;

namespace TBImplementation.Editor
{
    [CustomPropertyDrawer(typeof(UIButtonModel))]
    public class UIButtonDrawer : PropertyDrawer
    {
        private readonly string[] showOptions =
        {
            "Sprite",
            "Dimensions"
        };

        private GUIStyle popupStyle;

        /// <summary>
        /// Changes the appearance of our custom datatype, UIButtonModel. The function adds a dropdown to switch between 
        /// different settings, and makes it so that everything is visualy better in the editor.
        /// </summary>
        /// <param name="position">Position inside a Unity.Window.</param>
        /// <param name="property">The property we are changing.</param>
        /// <param name="label">The labelfield of the property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            SerializedProperty useConstant = property.FindPropertyRelative("showSpriteInput");

            Rect contentPosition = EditorGUI.PrefixLabel(position, label);
            int index = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, showOptions, popupStyle);
            useConstant.boolValue = index == 0;

            if (index == 1)
            {
                label = EditorGUI.BeginProperty(position, label, property);
                contentPosition.width *= 0.5f;
                EditorGUI.indentLevel = 0;
                EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("Position"), GUIContent.none);
                contentPosition.x += contentPosition.width;
                contentPosition.width /= 2f;
                EditorGUIUtility.labelWidth = 14f;
                EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("Width"), new GUIContent("W"));
                contentPosition.x += contentPosition.width;
                EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("Height"), new GUIContent("H"));
                EditorGUI.EndProperty();
            }
            else
            {
                label = EditorGUI.BeginProperty(position, label, property);
                EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("ButtonSprite"), GUIContent.none);
                EditorGUI.EndProperty();
            }
        }

        /// <summary>
        /// Changed the height of our property so we have more control over the space we want to use.
        /// </summary>
        /// <returns>The property height based on window width.</returns>
        /// <param name="property">Property.</param>
        /// <param name="label">Label.</param>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return Screen.width < 333 ? (16f + 18f) : 16f;
        }
    }
}