using System.Collections.Generic;
using UnityEngine;

namespace YadikkFramework.UI
{
    public class UIManager : SingletonPersistent<UIManager>
    {
        private Dictionary<UIPanelType, UIPanel> _registeredPanels = new Dictionary<UIPanelType, UIPanel>();
        private Stack<UIPanel> _panelHistory = new Stack<UIPanel>();
        
        public UIPanel CurrentPanel => _panelHistory.Count > 0 ? _panelHistory.Peek() : null;

        /// <summary>
        /// Registers a panel dynamically from the scene.
        /// Ensure to build a robust instancing system if panels are Prefabs loaded at runtime.
        /// </summary>
        public void RegisterPanel(UIPanel panel)
        {
            if (panel == null || panel.PanelType == UIPanelType.None)
            {
                Debug.LogWarning("[UIManager] Attempted to register an invalid or 'None' type UIPanel.");
                return;
            }

            if (!_registeredPanels.ContainsKey(panel.PanelType))
            {
                _registeredPanels.Add(panel.PanelType, panel);
                panel.Initialize();
            }
        }

        public void UnregisterPanel(UIPanel panel)
        {
            if (panel != null && _registeredPanels.TryGetValue(panel.PanelType, out UIPanel registered))
            {
                if (registered == panel)
                {
                    _registeredPanels.Remove(panel.PanelType);
                }
            }
        }

        public void ShowPanel(UIPanelType panelType, bool hideCurrent = true)
        {
            if (!_registeredPanels.TryGetValue(panelType, out UIPanel targetPanel))
            {
                Debug.LogWarning($"[UIManager] Cannot show Panel '{panelType}'; it is not registered.");
                return;
            }

            if (hideCurrent && _panelHistory.Count > 0)
            {
                UIPanel current = _panelHistory.Peek();
                if (current.PanelType == panelType)
                    return; // Already showing this panel

                current.Hide();
            }

            targetPanel.Show();
            _panelHistory.Push(targetPanel);
        }

        public void CloseCurrentPanel()
        {
            if (_panelHistory.Count == 0) return;

            UIPanel current = _panelHistory.Pop();
            current.Hide();

            if (_panelHistory.Count > 0)
            {
                UIPanel previous = _panelHistory.Peek();
                previous.Show();
            }
        }

        public void CloseAllPanels()
        {
            while (_panelHistory.Count > 0)
            {
                UIPanel current = _panelHistory.Pop();
                current.Hide();
            }
        }
    }
}
