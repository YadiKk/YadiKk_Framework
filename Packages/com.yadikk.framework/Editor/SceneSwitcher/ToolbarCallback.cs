using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace YadikkFramework.Editor
{
    [InitializeOnLoad]
    public static class ToolbarCallback
    {
        private static Type toolbarType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");
        private static ScriptableObject toolbar;

        public static Action OnToolbarGUI;

        static ToolbarCallback()
        {
            EditorApplication.update += OnUpdate;
        }

        private static void OnUpdate()
        {
            if (toolbar != null) return;

            var toolbars = Resources.FindObjectsOfTypeAll(toolbarType);
            if (toolbars.Length == 0) return;

            toolbar = (ScriptableObject)toolbars[0];

            var rootField = toolbarType.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
            var root = rootField.GetValue(toolbar) as VisualElement;

            if (root == null) return;

            var playModeZone = root.Q("ToolbarZonePlayMode");

            if (playModeZone == null) return;

            var container = new IMGUIContainer(() =>
            {
                try
                {
                    OnToolbarGUI?.Invoke();
                }
                catch { }
            });

            container.style.flexDirection = FlexDirection.Row;
            container.style.marginLeft = 5;

            
            playModeZone.Add(container);
        }
    }
}