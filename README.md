# YadiKk Framework Repository

This is the host repository for the **YadiKk Framework** UPM package.

## Installation
The framework is designed as a standalone Unity Package. You can install it directly from Git via the Unity Package Manager.

1. Open **Window > Package Manager** in Unity.
2. Click the `+` button in the top left and select **"Add package from git URL..."**.
3. Paste the following URL:
   `https://github.com/YadiKk/YadiKk_Framework.git?path=/Packages/com.yadikk.framework`

4. Click **Add**.

## Features

### Scene Selector (Editor Tool)
A powerful editor utility that automatically lists all scenes added to your **Build Settings**. 
* Access it via the Unity Editor to switch between scenes instantly during development.
* No more searching through folders to find your levels.

### Audio Management
A flexible audio system built on a **Persistent Singleton** architecture.
* **AudioManager:** Handles background music and SFX globally.
* **Ready-to-use Prefab:** Includes a pre-configured AudioManager prefab for quick setup.
* **Sound Search:** Efficiently find and play sounds by name using an optimized internal lookup.

## Demos & Samples
Once installed, you can import the "Demo and Prefabs" from the package's Samples section in the Package Manager to see these systems in action.
