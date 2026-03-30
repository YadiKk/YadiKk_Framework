using UnityEngine;

namespace YadikkFramework
{
    /// <summary>
    /// Advanced haptic feedback manager optimized for Android devices.
    /// Supports amplitude control for SDK 26+ and legacy fallback for older devices.
    /// </summary>
    public static class VibrationManager
    {
        #region Android Native Fields
#if UNITY_ANDROID && !UNITY_EDITOR
        private static readonly AndroidJavaObject vibrator;
        private static readonly int sdkVersion;

        static VibrationManager()
        {
            try
            {
                using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
                }
                sdkVersion = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"VibrationManager: Failed to initialize native Android vibrator. {e.Message}");
            }
        }
#endif
        #endregion

        #region Settings
        private static bool isVibrationEnabled = true;

        public enum VibrationStrength { Low, Normal, High }

        public enum VibrationEvent
        {
            None,
            Hit,
            Crash,
            Collect,
            Warning,
            UI_Click
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Triggers a vibration with specified strength and duration.
        /// On Android 8.0+, uses Amplitude for precise haptics.
        /// </summary>
        public static void Vibrate(VibrationStrength strength = VibrationStrength.Normal, long duration = 200)
        {
            if (!isVibrationEnabled) return;

#if UNITY_ANDROID && !UNITY_EDITOR
            if (vibrator == null) return;

            if (sdkVersion >= 26)
            {
                int amplitude = GetAmplitude(strength);
                using (AndroidJavaClass vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect"))
                {
                    AndroidJavaObject effect = vibrationEffectClass.CallStatic<AndroidJavaObject>(
                        "createOneShot", duration, amplitude);
                    vibrator.Call("vibrate", effect);
                }
            }
            else
            {
                vibrator.Call("vibrate", duration);
            }
#else
            Handheld.Vibrate();
#endif
        }

        /// <summary>
        /// Executes a predefined vibration pattern based on an in-game event.
        /// </summary>
        public static void Play(VibrationEvent vEvent)
        {
            switch (vEvent)
            {
                case VibrationEvent.Hit:
                    Vibrate(VibrationStrength.Normal, 60);
                    break;
                case VibrationEvent.Crash:
                    Vibrate(VibrationStrength.High, 400);
                    break;
                case VibrationEvent.Collect:
                    Vibrate(VibrationStrength.Low, 60);
                    break;
                case VibrationEvent.Warning:
                    Vibrate(VibrationStrength.Normal, 250);
                    break;
                case VibrationEvent.UI_Click:
                    Vibrate(VibrationStrength.Low, 30);
                    break;
            }
        }

        /// <summary>
        /// Stops any ongoing vibration immediately.
        /// </summary>
        public static void Cancel()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            vibrator?.Call("cancel");
#endif
        }

        public static void SetVibrationEnabled(bool enabled) => isVibrationEnabled = enabled;
        public static bool IsVibrationEnabled() => isVibrationEnabled;
        #endregion

        #region Internal Helpers
        private static int GetAmplitude(VibrationStrength strength)
        {
            return strength switch
            {
                VibrationStrength.Low => 60,
                VibrationStrength.Normal => 150,
                VibrationStrength.High => 255,
                _ => 150,
            };
        }
        #endregion
    }
}