using UnityEngine;
using UnityEngine.UI;
using YadikkFramework.State;

namespace YadikkFramework.UI.Panels
{
    public class PauseMenuPanel : UIPanel
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitToMenuButton;

        protected override void Awake()
        {
            base.Awake();
            
            if (resumeButton) resumeButton.onClick.AddListener(OnResumeClicked);
            if (settingsButton) settingsButton.onClick.AddListener(OnSettingsClicked);
            if (quitToMenuButton) quitToMenuButton.onClick.AddListener(OnQuitToMenuClicked);
        }

        private void Start()
        {
            UIManager.Instance?.RegisterPanel(this);
        }

        private void OnDestroy()
        {
            if (resumeButton) resumeButton.onClick.RemoveListener(OnResumeClicked);
            if (settingsButton) settingsButton.onClick.RemoveListener(OnSettingsClicked);
            if (quitToMenuButton) quitToMenuButton.onClick.RemoveListener(OnQuitToMenuClicked);
            
            if (UIManager.Instance != null)
                UIManager.Instance.UnregisterPanel(this);
        }

        private void OnResumeClicked()
        {
            UIManager.Instance.CloseCurrentPanel();
            GameStateManager.Instance.ResumeGame();
        }

        private void OnSettingsClicked()
        {
            UIManager.Instance.ShowPanel(UIPanelType.Settings, hideCurrent: false);
        }

        private void OnQuitToMenuClicked()
        {
            SceneLoader.Instance.LoadScene("Demo");

            UIManager.Instance.CloseAllPanels();
            GameStateManager.Instance.ResumeGame(); // Need time scale restored before switching
            GameStateManager.Instance.ChangeState(GameState.MainMenu);
            UIManager.Instance.ShowPanel(UIPanelType.MainMenu);
        }
    }
}
