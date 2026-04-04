using UnityEngine;
using UnityEngine.UI;
using YadikkFramework.State;

namespace YadikkFramework.UI.Panels
{
    public class MainMenuPanel : UIPanel
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button QuitButton;

        protected override void Awake()
        {
            base.Awake();
            
            if (playButton != null)
                playButton.onClick.AddListener(OnPlayClicked);
                
            if (settingsButton != null)
                settingsButton.onClick.AddListener(OnSettingsClicked);

               if (QuitButton != null)
                QuitButton.onClick.AddListener(OnQuitClicked);
        }

        private void Start()
        {
            // Register self with UIManager
            UIManager.Instance?.RegisterPanel(this);
        }

        private void OnDestroy()
        {
            if (playButton != null) playButton.onClick.RemoveListener(OnPlayClicked);
            if (settingsButton != null) settingsButton.onClick.RemoveListener(OnSettingsClicked);
            
            if (UIManager.Instance != null)
                UIManager.Instance.UnregisterPanel(this);
        }

        private void OnPlayClicked()
        {
            SceneLoader.Instance.LoadScene("GameIn");
            GameStateManager.Instance.ChangeState(GameState.Playing);
            UIManager.Instance.ShowPanel(UIPanelType.HUD);
        }

        void OnQuitClicked()
        {
            Application.Quit();
        }

        private void OnSettingsClicked()
        {
            // Opens Settings on top without hiding Main Menu completely if desired,
            // but standard flow expects hideCurrent = false if we want a popup behavior.
            // Using standard flow:
            UIManager.Instance.ShowPanel(UIPanelType.Settings, hideCurrent: false);
        }
    }
}
