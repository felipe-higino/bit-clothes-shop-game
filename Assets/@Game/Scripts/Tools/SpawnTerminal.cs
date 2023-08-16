using UnityEngine;
namespace Game.Scripts.Tools
{
    public static class SpawnTerminal
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Init()
        {
            GameObject terminalPrefab = Resources.Load<GameObject>("Terminal");
            GameObject terminalInstance = GameObject.Instantiate(terminalPrefab);
            GameObject.DontDestroyOnLoad(terminalInstance);
        }
    }
}