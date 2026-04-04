# Scene Management

The `SceneLoader` is a core utility designed to abstract away the repetitive requirements of switching Unity levels. It simplifies asynchronous native mapping and ensures rigid memory release transitions across levels.

## Implementation Details

`SceneLoader.cs` behaves structurally as an extension handling standard `UnityEngine.SceneManagement` mechanics but enforces Singleton patterns forcing memory sweeps logically without stalling the main UI threads violently.

## Usage Guide

Integrating standard transitions is as straightforward as single-line invocations anywhere in your codebase.

### Method 1: Loading by Name (Recommended)
You can directly call exact structural string names of indexed elements in the Build Settings.

```csharp
using YadikkFramework.Core;

// Instantly fires load sequence
SceneLoader.Instance.LoadScene("GameIn");
```

### Method 2: Loading by Build Index
Preferable for sequential level logic architectures where parsing strings isn't clean:

```csharp
// Loads the Build Target #1
SceneLoader.Instance.LoadScene(1);
```

### Advanced Considerations
*If you aim to generate Loading screens, the framework is heavily suitable to inject an `OnLoadBegin` and `OnLoadEnd` Action broadcaster within `SceneLoader.cs` that flags the `UIManager`.*
