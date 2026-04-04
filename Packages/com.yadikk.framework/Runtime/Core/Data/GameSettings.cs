using System;

namespace YadikkFramework.Data
{
    [Serializable]
    public class GameSettings
    {
        public float MasterVolume = 1.0f;
        public float MusicVolume = 1.0f;
        public float SfxVolume = 1.0f;
        
        public bool IsVibrationEnabled = true;

        public int QualityLevel = 2; // e.g., 0: Low, 1: Medium, 2: High
        public string LanguageCode = "en";
    }
}
