using UnityEngine;

namespace NGUIndustriesInjector
{
    public class Loader
    {
        private static GameObject _load;
        public static void Init()
        {
            _load = new GameObject();
            _load.AddComponent<Main>();
            Object.DontDestroyOnLoad(_load);
        }

        public static void Unload()
        {
            _Unload();
        }

        private static void _Unload()
        {
            Main.reference.Unload();
            _load.SetActive(false);
            Object.Destroy(_load);
        }
    }
}
