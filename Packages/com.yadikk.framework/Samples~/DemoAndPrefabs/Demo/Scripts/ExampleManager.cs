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
}
