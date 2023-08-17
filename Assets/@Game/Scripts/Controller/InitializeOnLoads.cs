using Game.Scripts.View;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class InitializeOnLoads
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Init()
        {
            GameObject root = new("[InitializeOnLoads]");
            GameObject.DontDestroyOnLoad(root);

            CursorManager cursorManagerPrefab = Resources.Load<CursorManager>("CursorManager");
            GameObject.Instantiate(cursorManagerPrefab, root.transform);

            AudioController audioControllerPrefab = Resources.Load<AudioController>("AudioController");
            GameObject.Instantiate(audioControllerPrefab, root.transform);

            GameObject terminalPrefab = Resources.Load<GameObject>("Terminal");
            GameObject.Instantiate(terminalPrefab, root.transform);
        }

    }
}