using System;
using UnityEngine;

namespace YadikkFramework.Data
{
    public class SettingsManager : SingletonPersistent<SettingsManager>
    {
        private const string SETTINGS_PREFS_KEY = "YadikkFramework_GameSettings";

        public GameSettings CurrentSettings { get; private set; }

        /// <summary>
        /// Triggered whenever settings are saved/updated.
        /// </summary>
        public event Action<GameSettings> OnSettingsUpdated;

        protected override void Awake()
        {
            base.Awake();
            LoadSettings();
        }

        public void LoadSettings()
        {
            if (PlayerPrefs.HasKey(SETTINGS_PREFS_KEY))
            {
                string json = PlayerPrefs.GetString(SETTINGS_PREFS_KEY);
                CurrentSettings = JsonUtility.FromJson<GameSettings>(json) ?? new GameSettings();
            }
            else
            {
                CurrentSettings = new GameSettings();
                SaveSettings(); // Save default settings
            }

            OnSettingsUpdated?.Invoke(CurrentSettings);
        }

        public void SaveSettings()
        {
            if (CurrentSettings == null)
            {
                CurrentSettings = new GameSettings();
            }

            string json = JsonUtility.ToJson(CurrentSettings);
            PlayerPrefs.SetString(SETTINGS_PREFS_KEY, json);
            PlayerPrefs.Save();

            OnSettingsUpdated?.Invoke(CurrentSettings);
        }

        // --- Helper Methods to Mutate & Save Safely ---

        public void SetMasterVolume(float volume)
        {
            CurrentSettings.MasterVolume = Mathf.Clamp01(volume);
            SaveSettings();
        }

        public void SetMusicVolume(float volume)
        {
            CurrentSettings.MusicVolume = Mathf.Clamp01(volume);
            SaveSettings();
        }

        public void SetSfxVolume(float volume)
        {
            CurrentSettings.SfxVolume = Mathf.Clamp01(volume);
            SaveSettings();
        }

        public void SetVibration(bool enabled)
        {
            CurrentSettings.IsVibrationEnabled = enabled;
            SaveSettings();
        }
    }
}
