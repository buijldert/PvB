using UnityEngine;

namespace Parkers.Editor.SceneView
{
    using UnityEditor;

    public enum ButtonIndex
    {
        Pause,
        OneEighth,
        Quarter,
        Half,
        Normal,
        OneHalf,
        Dubbel
    }

    [InitializeOnLoad]
    public class TimeScaleEditor : Editor
    {
        private static int SelectedTimeScale = 4;
        private const float DEFAULT_TIME_SCALE = 1f;

        static TimeScaleEditor()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }

        private static void OnSceneGUI(SceneView sceneView)
        {
            DrawMenu(sceneView.position);
            ChangeTimeScale();
        }

        private static void DrawMenu(Rect position)
        {
            Handles.BeginGUI();

            GUILayout.BeginArea(new Rect(0, position.height - 35, position.width, 20), EditorStyles.toolbar);
            {
                string[] buttonLabels =
                {
                    "||",
                    "×⅛",
                    "×¼",
                    "×½",
                    "×1",
                    "×1½",
                    "×2"
                };

                SelectedTimeScale = GUILayout.SelectionGrid
                    (
                        SelectedTimeScale,
                        buttonLabels,
                        7,
                        EditorStyles.toolbarButton,
                        GUILayout.Width(300)
                    );
            }
            GUILayout.EndArea();

            Handles.EndGUI();
        }

        private static void ChangeTimeScale()
        {
            switch (SelectedTimeScale)
            {
                case (int)ButtonIndex.Pause:
                    Time.timeScale = 0;
                    break;
                case (int)ButtonIndex.OneEighth:
                    Time.timeScale = DEFAULT_TIME_SCALE / 8;
                    break;
                case (int)ButtonIndex.Quarter:
                    Time.timeScale = DEFAULT_TIME_SCALE / 4;
                    break;
                case (int)ButtonIndex.Half:
                    Time.timeScale = DEFAULT_TIME_SCALE / 2;
                    break;
                case (int)ButtonIndex.Normal:
                    Time.timeScale = DEFAULT_TIME_SCALE;
                    break;
                case (int)ButtonIndex.OneHalf:
                    Time.timeScale = DEFAULT_TIME_SCALE * 1.5f;
                    break;
                case (int)ButtonIndex.Dubbel:
                    Time.timeScale = DEFAULT_TIME_SCALE * 2f;
                    break;
            }
        }

        private void OnDestroy()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
        }
    }
}