using UnityEngine;
using YadikkFramework;

public class ExampleManager : MonoBehaviour
{
    public void StartMusic()
    {
        if (AudioManager.Instance == null)
        {
            return;
        }
        AudioManager.Instance.PlayMusic("Background", false);
    }

    public void StartSFX()
    {
        if (AudioManager.Instance == null)
        {
            return;
        }

        AudioManager.Instance.PlaySFX("ButtonClick");
    }

    public void StopMusic()
    {
        if (AudioManager.Instance == null)
        {
            return;
        }
        AudioManager.Instance.StopMusic();
    }

    public void VibrateH()
    {
        VibrationManager.Vibrate(VibrationManager.VibrationStrength.High, 300);
    }
    public void VibrateN()
    {
        VibrationManager.Vibrate(VibrationManager.VibrationStrength.Normal, 300);
    }
    public void VibrateL()
    {
        VibrationManager.Vibrate(VibrationManager.VibrationStrength.Low, 300);
    }
}
