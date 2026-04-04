# YadiKk Framework Documentation

Welcome to the **YadiKk Framework** documentation. This framework is a scalable, module-based architecture designed specifically to accelerate the development of mobile and hyper-casual games in Unity.

It provides a suite of battle-tested systems including UI Management, State Control, Persistent Data Saving, Audio Handling, and Scene Management—all tightly decoupled and cleanly organized into a Unity Package Manager (UPM) standard structure.

---

## 🚀 Quick Start Guide

### 1. Installation

You can install this package natively via the Unity Package Manager using the Git URL implementation.

1. Open **Window > Package Manager** in Unity.
2. Click the `+` button in the top left and select **Add package from git URL...**
3. Paste: `https://github.com/USERNAME/REPO.git?path=/Packages/com.yadikk.framework`
4. Click **Add**.

### 2. Standard Hierarchy Setup

For the optimal mobile experience, we highly recommend setting up a single Persistent Bootstrapper scene or utilizing an architecture that keeps Managers alive across all your levels using `SingletonPersistent`.

Create the following hierarchy in your root initialization scene:

```
▼ [MANAGERS] (Empty GameObject)
    ↳ GameStateManager
    ↳ SettingsManager
    ↳ GameBootstrapper

▼ [GLOBAL_UI_CANVAS] (Canvas)
    ↳ UIManager
    ▼ Panels
        ↳ MainMenuPanel
        ↳ SettingsPanel
        ↳ HUDPanel
        ↳ PauseMenuPanel
```
*Note: Because the core managers inherit from `SingletonPersistent`, they will use `DontDestroyOnLoad` and safely persist across any scene changes seamlessly.*

### 3. Usage Examples

- **Changing Game States:**
  ```csharp
  GameStateManager.Instance.ChangeState(GameState.Playing);
  ```
- **Opening a UI Panel:**
  ```csharp
  UIManager.Instance.ShowPanel(UIPanelType.Settings, hideCurrent: false);
  ```
- **Loading a Scene:**
  ```csharp
  SceneLoader.Instance.LoadScene("Level_01");
  ```

---

## 📚 Module Reference

Delve into our specialized modules below for in-depth insights, code snippets, and setup guides.

- [State Management](state-management.md)
- [UI Architecture](ui.md)
- [Persistent Save System](save-system.md)
- [Scene Management](scene-management.md)
- [Audio & Vibration](audio.md)

---
*YadiKk Framework v1.0.x — Built for performance and scalability.*
