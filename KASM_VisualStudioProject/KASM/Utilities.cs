using KSP;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace KASM
{
    public static class Utilities
    {
        // Constants
        public static readonly string assetsPath = Path.Combine(KSPUtil.ApplicationRootPath, "GameData", "KASM", "Assets");
        public const string iconPath = "KASM/Assets/icon_128x128";
        public const string debugName = "[KASM]";

        public static AssetBundle LoadAssetBundle(string assetBundleName)
        {
            string name = string.Empty;
            switch (Application.platform)
            {
                case RuntimePlatform.OSXPlayer:
                    name = $"{assetBundleName}_osx.assetbundle";
                    break;
                case RuntimePlatform.WindowsPlayer:
                    name = $"{assetBundleName}_win64.assetbundle";
                    break;
                case RuntimePlatform.LinuxPlayer:
                    name = $"{assetBundleName}_linux64.assetbundle";
                    break;
            }

            string path = Path.Combine(assetsPath, name);

            if (!File.Exists(path))
            {
                Debug.LogError($"{debugName} Assetbundle {path} is missing!");
                return null;
            }

            AssetBundle resourcesAB = AssetBundle.LoadFromFile(path);

            if (resourcesAB == null) // just to be extra safe
            {
                Debug.LogError($"{debugName} Error when loading {path}!");
                return null;
            }

            return resourcesAB;
        }

        public static T SafeLoadFromAssetBundle<T>(AssetBundle assetBundle, string name) where T : Object
        {
            T asset = assetBundle.LoadAsset<T>(name);
            if (!asset)
                Debug.LogError($"{debugName} {name} is missing in asset bundle!");

            return asset;
        }

        public static T GetComponentOnChild<T>(Transform parent, string targetPath)
        {
            return parent.Find(targetPath).gameObject.GetComponent<T>();
        }

        public static T AddComponentOnChild<T>(Transform parent, string targetPath) where T : Component
        {
            return parent.Find(targetPath).gameObject.AddComponent<T>();
        }
    }
}
