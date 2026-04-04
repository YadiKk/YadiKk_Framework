# Persistent Save System

The built-in Persistent Save mechanisms handle seamless synchronization of runtime application data directly onto device storage without rigid Database reliance requirements perfectly suited for straightforward Mobile game attributes.

## Internal Logistics

It strictly utilizes native `JsonUtility` serialized structures masked under `PlayerPrefs`. This provides:
- Instant native cross-OS execution compatibility.
- Zero IO wait times compared to deep file path extractions.
- Extensible structures allowing infinite scalability based strictly via modifying `.cs` DTOs.

## The Model: GameSettings
Settings elements are stored in a standard data envelope:

```csharp
[Serializable]
public class GameSettings
{
    public float MasterVolume = 1.0f;
    public bool IsVibrationEnabled = true;
    public string LanguageCode = "en";
}
```
*To add a standard definition, explicitly add custom attributes here. The system manages the Rest.*

## Manipulating Information

All access strictly channels through the `SettingsManager.cs` interface guaranteeing broadcast notifications when values drift.

### Fetching Cached Data
```csharp
// Grab instantaneous configuration representations
float userMusicVol = SettingsManager.Instance.CurrentSettings.MusicVolume;
```

### Pushing Hardcoded Updates
```csharp
// Saves internally instantly and invokes Global Actions 
SettingsManager.Instance.SetVibration(false);
```

### Binding Dynamic UI Updates
Your UI elements shouldn't pull loops; they just subscribe!

```csharp
private void OnEnable()
{
    SettingsManager.Instance.OnSettingsUpdated += ReRenderToggleParameters;
}

private void ReRenderToggleParameters(GameSettings dataPool)
{
    uiToggleBox.isOn = dataPool.IsVibrationEnabled;
}
```
