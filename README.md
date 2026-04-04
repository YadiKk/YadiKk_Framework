<br/>
<div align="center">
  <h1 align="center">YadiKk Framework</h1>
  <p align="center">
    <strong>A professional, modular, and highly scalable toolkit for Unity Mobile & Hyper-Casual development.</strong>
  </p>
</div>

---

## Welcome

Welcome to the host repository for the **YadiKk Framework**. This library is strictly formatted as a Unity Package Manager (UPM) package, ensuring that your core game logic remains clean, separate from library code, and easily updatable across all your projects.

## Core Features

*   ** Game State Management:** Safe macro-state control with decoupled UI pausing (`Time.timeScale` safe).
*   ** Persistent Save System:** Native `JsonUtility` & `PlayerPrefs` wrapper that auto-loads your custom settings out-of-the-box.
*   ** Modular UI Architecture:** Panel-based navigation stack with CanvasGroup logic (no expensive `Rebuilds`).
*   ** Scene Management:** Streamlined level loading APIs.
*   ** Audio & Haptics:** Native vibration integration tailored for high-performance mobile execution.
*   ** Editor Tools:** Quick 'SceneSwitcher' toolbar drop-down right inside your Unity Editor.

##  Installation

You can install this framework directly from GitHub into any Unity Project without downloading raw files.

1. Open **Window > Package Manager** in Unity.
2. Click the `+` button in the top left and select **Add package from git URL...**
3. Paste the following URL:
   ```text
   https://github.com/USERNAME/REPO.git?path=/Packages/com.yadikk.framework
   ```
   *(Update `USERNAME/REPO` to match the exact GitHub repository URL before pasting)*
4. Click **Add**.

##  Documentation

We provide extensive, sector-standard documentation integrated directly into the Package Manager.

 **[Read the Full Documentation Here](Packages/com.yadikk.framework/Documentation~/index.md)**

Or access it inside Unity:
*   Once installed, select the **YadiKk Framework** in the Package Manager.
*   Click **View Documentation** to open the integrated markdown webview.

##  Samples & Prefabs

The package comes bundled with ready-to-use prefabs and demo scenes. 
*   Open the **Package Manager**.
*   Select **YadiKk Framework**.
*   Navigate to the **Samples** tab and click **Import** next to "Demo and Prefabs".

---
<div align="center">
  <sub>Built for Performance. Designed for Scale.</sub>
</div>
