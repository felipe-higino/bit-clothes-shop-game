using System;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public static class Service<T> where T : class, new()
    {
        static T _instance;

        public static T Get()
        {
            if (null == _instance)
            {
                _instance = new T();
                Disposer.OnDispose += Dispose;
            }

            return _instance;
        }

        static void Dispose()
        {
            _instance = null;
            Disposer.OnDispose -= Dispose;
        }
    }

    class Disposer : MonoBehaviour
    {
        public static event Action OnDispose;

        void OnDestroy()
        {
            OnDispose?.Invoke();
        }

        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            GameObject go = new("[DISPOSER]");
            DontDestroyOnLoad(go);
            go.AddComponent<Disposer>();
        }
    }

}