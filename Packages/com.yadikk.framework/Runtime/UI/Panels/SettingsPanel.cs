using UnityEngine;
using UnityEngine.UI;
using YadikkFramework.Data;

namespace YadikkFramework.UI.Panels
{
    public class SettingsPanel : UIPanel
    {
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Toggle vibrationToggle;
        [SerializeField] private Button closeButton;

        private bool _isInitializing = false;

        protected override void Awake()
        {
            base.Awake();
            
            if (masterVolumeSlider) masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            if (musicVolumeSlider) musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            if (sfxVolumeSlider) sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
            if (vibrationToggle) vibrationToggle.onValueChanged.AddListener(OnVibrationChanged);
            
            if (closeButton) closeButton.onClick.AddListener(OnCloseClicked);
        }

        private void Start()
        {
            UIManager.Instance?.RegisterPanel(this);
        }

        private void OnDestroy()
        {
            if (masterVolumeSlider) masterVolumeSlider.onValueChanged.RemoveListener(OnMasterVolumeChanged);
            if (musicVolumeSlider) musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            if (sfxVolumeSlider) sfxVolumeSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
            if (vibrationToggle) vibrationToggle.onValueChanged.RemoveListener(OnVibrationChanged);
            
            if (closeButton) closeButton.onClick.RemoveListener(OnCloseClicked);
            
            if (UIManager.Instance != null)
                UIManager.Instance.UnregisterPanel(this);
        }

        public override void Show()
        {
            RefreshUIFromSettings();
            base.Show();
        }

        private void RefreshUIFromSettings()
        {
            if (SettingsManager.Instance == null) return;
            
            _isInitializing = true;
            
            GameSettings settings = SettingsManager.Instance.CurrentSettings;
            if (masterVolumeSlider) masterVolumeSlider.value = settings.MasterVolume;
            if (musicVolumeSlider) musicVolumeSlider.value = settings.MusicVolume;
            if (sfxVolumeSlider) sfxVolumeSlider.value = settings.SfxVolume;
            if (vibrationToggle) vibrationToggle.isOn = settings.IsVibrationEnabled;
            
            _isInitializing = false;
        }

        private void OnMasterVolumeChanged(float value)
        {
            if (_isInitializing) return;
            SettingsManager.Instance.SetMasterVolume(value);
        }

        private void OnMusicVolumeChanged(float value)
        {
            if (_isInitializing) return;
            SettingsManager.Instance.SetMusicVolume(value);
        }

        private void OnSfxVolumeChanged(float value)
        {
            if (_isInitializing) return;
            SettingsManager.Instance.SetSfxVolume(value);
        }

        private void OnVibrationChanged(bool isOn)
        {
            if (_isInitializing) return;
            SettingsManager.Instance.SetVibration(isOn);
        }

        private void OnCloseClicked()
        {
            UIManager.Instance.CloseCurrentPanel();
        }
    }
}
