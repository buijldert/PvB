using UnityEditor;
using UnityEngine;
using TBImplementation.ScriptableObjects;

namespace TBImplementation.Editor
{
    [CustomEditor(typeof(UITheme))]
    [CanEditMultipleObjects]
    public class UIThemeEditor : UnityEditor.Editor
    {
        /// <summary>
        /// With this function we can change the appearance of the ScriptableObject for better visual feedback.
        /// This visiual feedback is called the StaticPreview.
        /// </summary>
        /// <returns>The static preview.</returns>
        /// <param name="assetPath">Asset path to the object.</param>
        /// <param name="subAssets">Sub assets of the object.</param>
        /// <param name="width">Width of the StaticPreview.</param>
        /// <param name="height">Height of the StaticPreview.</param>
        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            UITheme theme = (UITheme)target;
            Sprite icon = theme.icon;

            Texture2D preview = AssetPreview.GetAssetPreview(icon);
            Texture2D final = new Texture2D(width, height);

            if (preview != null)
            {
                EditorUtility.CopySerialized(preview, final);
            }
            else
            {
                preview = EditorGUIUtility.FindTexture("ScriptableObject Icon");
                EditorUtility.CopySerialized(preview, final);
            }

            return final;
        }
    }
}
