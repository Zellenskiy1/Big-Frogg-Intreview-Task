using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    internal class IntroductionWindow : EditorWindow
    {
        private const string HasShownKey = "IntroductionWindowHasShown";

        static IntroductionWindow()
        {
            EditorApplication.update += ShowOnStartup;
        }

        private static void ShowOnStartup()
        {
            if (!SessionState.GetBool(HasShownKey, false))
            {
                SessionState.SetBool(HasShownKey, true);

                var window = GetWindow<IntroductionWindow>(true, "Introduction");
                window.minSize = new Vector2(450, 180);
                window.maxSize = new Vector2(450, 180); 
            }

            EditorApplication.update -= ShowOnStartup;
        }

        private void OnGUI()
        {
            GUILayout.Space(20);

            var labelStyle = new GUIStyle(GUI.skin.label)
            {
                wordWrap = true,
                alignment = TextAnchor.MiddleCenter,
                fontSize = 13
            };

            GUILayout.Label(
                "For the best experience, please set your Game view resolution to 1920x1080.\nThank you and have fun!",
                labelStyle);

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("OK", GUILayout.Width(120), GUILayout.Height(45)))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Gameplay.unity");
                Close();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
        }
    }
}