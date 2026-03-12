using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// Base class for UI panels managed by UIManager.
    /// Attach this to the root GameObject of each UI panel.
    /// </summary>
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private string _panelName;

        /// <summary>
        /// Unique name identifier for this panel.
        /// </summary>
        public string PanelName => _panelName;

        /// <summary>
        /// Shows this panel.
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        /// <summary>
        /// Hides this panel.
        /// </summary>
        public virtual void Hide()
        {
            OnHide();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Called when the panel is shown. Override for custom behavior.
        /// </summary>
        protected virtual void OnShow() { }

        /// <summary>
        /// Called when the panel is hidden. Override for custom behavior.
        /// </summary>
        protected virtual void OnHide() { }
    }
}
