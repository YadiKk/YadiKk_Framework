# State Management

The State Management module handles the high-level macro states of your game. It guarantees strict flow control (preventing simultaneous "Playing" and "Paused" logic from conflicting) and broadcasts real-time transition events to decoupled systems.

## Key Concepts

- **GameState Enum:** Pre-defined robust states (`Boot`, `MainMenu`, `Playing`, `Paused`, `GameOver`).
- **SingletonPersistent:** The Manager natively survives scene loads across the application lifecycle.
- **Time Scaling:** Automatically handles `Time.timeScale` freezing for pauses, keeping UI components interacting via `unscaledDeltaTime` responsive.

## Setup

Attach the `GameStateManager.cs` onto a core Boot or Manager GameObject. No inspector adjustments are strictly required.

## Code Snippets

### Triggering a State Change
You can explicitly demand a state shift anywhere in your logic:

```csharp
using YadikkFramework.State;

public class LevelEnder : MonoBehaviour
{
    private void TriggerWin()
    {
        GameStateManager.Instance.ChangeState(GameState.GameOver);
    }
}
```

### Pausing and Resuming Safely
Instead of manually tweaking time scales which can produce destructive gameplay side-effects, use the built-in pause operations:

```csharp
// Freeze game progression and invoke pause state events
GameStateManager.Instance.PauseGame();

// Unfreeze progression and revert to the previous state automatically
GameStateManager.Instance.ResumeGame();
```

### Subscribing to State Changes
Event-driven pipelines can listen to these state changes without polling `Update` loops:

```csharp
private void OnEnable()
{
    GameStateManager.Instance.OnGameStateChanged += HandleStateChanged;
}

private void OnDisable()
{
    if (GameStateManager.Instance != null)
        GameStateManager.Instance.OnGameStateChanged -= HandleStateChanged;
}

private void HandleStateChanged(GameState previousState, GameState newState)
{
    if (newState == GameState.Playing)
    {
        // Un-lock player controllers!
    }
}
```
