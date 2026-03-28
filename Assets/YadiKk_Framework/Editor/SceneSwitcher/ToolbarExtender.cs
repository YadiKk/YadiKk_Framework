using System;
using System.Collections.Generic;
using UnityEngine;

namespace YadikkFramework.Editor
{
    public static class ToolbarExtender
    {
        public static List<Action> RightToolbarGUI = new List<Action>();

        static ToolbarExtender()
        {
            ToolbarCallback.OnToolbarGUI += Draw;
        }

        private static void Draw()
        {
            GUILayout.BeginHorizontal();

            foreach (var handler in RightToolbarGUI)
                handler?.Invoke();

            GUILayout.EndHorizontal();
        }
    }
}