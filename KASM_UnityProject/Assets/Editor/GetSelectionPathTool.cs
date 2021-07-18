using UnityEngine;
using UnityEditor;
using System.Collections;

public class GetSelectionPathTool
{
    [MenuItem("Tools/Get Selection Path")]
    public static void GetSelectionPath()
    {
        Transform target = Selection.activeTransform;


        string path = target.gameObject.name;
        while (target.parent != null)
        {
            target = target.parent;
            if (target.gameObject.name == "Window")
                break;
            path = target.name + "/" + path;
        }

        Debug.Log("Saved to copy buffer: " + path);
        EditorGUIUtility.systemCopyBuffer = path;
    }
}
