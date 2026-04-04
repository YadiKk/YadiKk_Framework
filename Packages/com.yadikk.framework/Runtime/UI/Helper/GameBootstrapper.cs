using System.Collections;
using UnityEngine;
using YadikkFramework.State;
using YadikkFramework.UI;

namespace YadikkFramework
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IEnumerator Start()
        {
            //wait a frame to ensure all singletons are initialized and Awake() methods have run
            yield return null;
            GameStateManager.Instance.ChangeState(GameState.MainMenu);
            UIManager.Instance.ShowPanel(UIPanelType.MainMenu);
        }
    }

}
