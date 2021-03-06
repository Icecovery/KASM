using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleCompiler
{
    [MenuItem("Tools/Build Selected AssetBundle")]
    static void BuildAssetBundleWin64()
    {
        string path = EditorUtility.SaveFilePanel("Build Asset Bundle", Path.Combine("..", "GameData", "KASM", "Assets"), "KASM_resources", "assetbundle");

        ExportAssetBundle(BuildTarget.StandaloneWindows64, path);
        //TODO Uncomment these lines on the final build
        //ExportAssetBundle(BuildTarget.StandaloneOSX, path);
        //ExportAssetBundle(BuildTarget.StandaloneLinux64, path);
    }

    private static void ExportAssetBundle(BuildTarget target, string path)
    {
        string name = Path.GetFileNameWithoutExtension(path);
        string directory = Path.GetDirectoryName(path);
        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        AssetBundleBuild build = new AssetBundleBuild();

        switch (target)
        {
            case BuildTarget.StandaloneOSX:
                build.assetBundleName = name + "_osx.assetbundle";
                break;
            case BuildTarget.StandaloneWindows64:
                build.assetBundleName = name + "_win64.assetbundle";
                break;
            case BuildTarget.StandaloneLinux64:
                build.assetBundleName = name + "_linux64.assetbundle";
                break;
            default:
                build.assetBundleName = name + ".assetbundle";
                break;
        }

        //build.assetBundleName = name;
        build.assetNames = new string[selection.Length];
        int len = selection.Length;
        for (int i = 0; i < len; i++)
        {
            build.assetNames[i] = AssetDatabase.GetAssetPath(selection[i]);
            MonoBehaviour.print("Building asset: " + build.assetNames[i]);
        }
        BuildPipeline.BuildAssetBundles(directory, new AssetBundleBuild[] { build }, BuildAssetBundleOptions.None, target);
    }

}
