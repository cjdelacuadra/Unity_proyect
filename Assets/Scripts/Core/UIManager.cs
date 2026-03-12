using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// Manages UI panels, handling show/hide and panel stacking.
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private List<UIPanel> _panels = new List<UIPanel>();

        private readonly Dictionary<string, UIPanel> _panelMap = new Dictionary<string, UIPanel>();
        private readonly Stack<UIPanel> _panelStack = new Stack<UIPanel>();

        protected override void OnInitialize()
        {
            foreach (var panel in _panels)
            {
                if (panel != null)
                {
                    _panelMap[panel.PanelName] = panel;
                    panel.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Shows a panel by name. Optionally hides the current top panel.
        /// </summary>
        /// <param name="panelName">Name of the panel to show.</param>
        /// <param name="hideCurrent">Whether to hide the currently active panel.</param>
        public void ShowPanel(string panelName, bool hideCurrent = true)
        {
            if (!_panelMap.TryGetValue(panelName, out var panel))
            {
                Debug.LogWarning($"[UIManager] Panel '{panelName}' not found.");
                return;
            }

            if (hideCurrent && _panelStack.Count > 0)
            {
                var current = _panelStack.Peek();
                current.Hide();
            }

            _panelStack.Push(panel);
            panel.Show();
        }

        /// <summary>
        /// Hides the topmost panel and shows the one beneath it.
        /// </summary>
        public void GoBack()
        {
            if (_panelStack.Count == 0) return;

            var current = _panelStack.Pop();
            current.Hide();

            if (_panelStack.Count > 0)
            {
                var previous = _panelStack.Peek();
                previous.Show();
            }
        }

        /// <summary>
        /// Hides all panels and clears the stack.
        /// </summary>
        public void HideAll()
        {
            while (_panelStack.Count > 0)
            {
                var panel = _panelStack.Pop();
                panel.Hide();
            }
        }

        /// <summary>
        /// Registers a panel at runtime.
        /// </summary>
        /// <param name="panel">The panel to register.</param>
        public void RegisterPanel(UIPanel panel)
        {
            if (panel != null && !_panelMap.ContainsKey(panel.PanelName))
            {
                _panelMap[panel.PanelName] = panel;
                _panels.Add(panel);
            }
        }
    }
}
