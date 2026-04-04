# Audio & Vibration Systems

YadiKk Framework natively encapsulates device hardware APIs ensuring optimal battery consumption while delivering instantaneous responsive Haptics and Audio pipelines mapped dynamically to the persistent settings interface.

## Vibration Manager

The `VibrationManager` controls translating generic unity input requirements directly onto device APIs if enabled by the user preferences.

### Execution

Simply invoke anywhere inside your components without requiring explicit cache checks. The Manager reads the global state dynamically and drops executions if users have the configuration disabled!

```csharp
using YadikkFramework.Core;

public class Explosion : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        // Automatically skips if SettingsManager.Instance.CurrentSettings.IsVibrationEnabled == false;
        VibrationManager.TriggerVibrate();
    }
}
```

## Audio Pipelines (Upcoming)

*This module specifically serves as the entry node for upcoming structured `AudioManager` enhancements which will automatically parse `GameSettings` Volume Sliders natively mapped directly to Unity `AudioMixerGroups` out of the box.*
