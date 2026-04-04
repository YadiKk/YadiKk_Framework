# UI Architecture

The UI architecture in YadiKk Framework replaces costly heavy scene-loaded Canvas initializations with a highly structured, scalable Panel system that utilizes `CanvasGroups` to hide and show visual context virtually context-free.

## Key Features

- **Decoupled Architecture:** Business logic lives entirely separated from visual drawing.
- **CanvasGroup Adaptations:** Prevents painful internal `LayoutRebuilder` lag by merely un-checking raycasts and alpha elements vs destroying root objects entirely.
- **Panel History Stack:** Back-button navigation memory is handled out of the box dynamically via stack tracking.

## Setup & Integration

1. Attach `UIManager.cs` directly to your global `Canvas` container.
2. Ensure you have the `UIPanel` base classes correctly attached to every container.
3. Every UI panel must be verified as **Active in Hierarchy** before saving the scene. 
> [!NOTE]
> *Why Active?* During `Start()`, `UIManager` requests panels to identify themselves natively. If they are unchecked, Unity suppresses `Start()` firing, thus registering failures. The UIManager hides them for you locally in milliseconds!

## Usage Actions

### Implementing a New Custom Panel
Override the abstract concepts to map out distinct UI interactions cleanly:

```csharp
using UnityEngine;
using YadikkFramework.UI;

public class ShopPanel : UIPanel
{
    protected override void Awake()
    {
        base.Awake(); // Grabs the mandatory CanvasGroup!
    }

    private void Start() 
    {
        UIManager.Instance?.RegisterPanel(this);
    }

    private void OnDestroy() 
    {
        if (UIManager.Instance != null)
            UIManager.Instance.UnregisterPanel(this);
    }

    public override void Show()
    {
        RefreshShopItems();
        base.Show(); 
    }
}
```

### Displaying a Target Panel
Use the global UI orchestrator to pop interfaces:

```csharp
// Completely takes over focus and hides the active canvas layer
UIManager.Instance.ShowPanel(UIPanelType.Settings);

// Opens interface 'Pop-Up' style ontop of existing content
UIManager.Instance.ShowPanel(UIPanelType.PauseMenu, hideCurrent: false);
```

### Navigating History
If the user demands back-navigational properties:

```csharp
// Pops the current panel and reverts viewing to the previous canvas stacked!
UIManager.Instance.CloseCurrentPanel();
```
