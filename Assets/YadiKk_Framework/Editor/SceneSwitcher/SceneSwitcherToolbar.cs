using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

namespace YadikkFramework.Editor
{
    [InitializeOnLoad]
    public static class SceneSwitcherToolbar
    {
        private static string[] sceneNames;
        private static string[] scenePaths;
        private static int selectedIndex;

        static SceneSwitcherToolbar()
        {
            LoadScenes();

            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);

            EditorSceneManager.activeSceneChangedInEditMode += OnSceneChanged;
            EditorBuildSettings.sceneListChanged += OnSceneListChanged;
        }

        private static void LoadScenes()
        {
            var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).ToArray();

            sceneNames = scenes
                .Select(s => System.IO.Path.GetFileNameWithoutExtension(s.path))
                .ToArray();

            scenePaths = scenes
                .Select(s => s.path)
                .ToArray();

            selectedIndex = GetCurrentSceneIndex();
        }

        private static void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            selectedIndex = GetCurrentSceneIndex();
        }

        private static void OnSceneListChanged()
        {
            LoadScenes();
        }

        private static int GetCurrentSceneIndex()
        {
            string currentPath = EditorSceneManager.GetActiveScene().path;

            for (int i = 0; i < scenePaths.Length; i++)
            {
                if (scenePaths[i] == currentPath)
                    return i;
            }

            return 0;
        }

        private static void OnToolbarGUI()
        {
            if (sceneNames == null || sceneNames.Length == 0)
                return;

            GUILayout.Space(5);

            int newIndex = EditorGUILayout.Popup(selectedIndex, sceneNames, GUILayout.Width(140));

            if (newIndex != selectedIndex)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    selectedIndex = newIndex;
                    EditorSceneManager.OpenScene(scenePaths[selectedIndex]);
                }
            }
        }
    }
}