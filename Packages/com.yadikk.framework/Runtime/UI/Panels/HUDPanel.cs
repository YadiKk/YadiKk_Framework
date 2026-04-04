using UnityEngine;
using UnityEngine.UI;
using YadikkFramework.State;

namespace YadikkFramework.UI.Panels
{
    public class HUDPanel : UIPanel
    {
        [SerializeField] private Button pauseButton;

        protected override void Awake()
        {
            base.Awake();
            
            if (pauseButton)
                pauseButton.onClick.AddListener(OnPauseClicked);
        }

        private void Start()
        {
            UIManager.Instance?.RegisterPanel(this);
        }

        private void OnDestroy()
        {
            if (pauseButton) pauseButton.onClick.RemoveListener(OnPauseClicked);
            
            if (UIManager.Instance != null)
                UIManager.Instance.UnregisterPanel(this);
        }

        private void OnPauseClicked()
        {
            UIManager.Instance.ShowPanel(UIPanelType.PauseMenu, hideCurrent: false);
            GameStateManager.Instance.PauseGame();
        }
    }
}
