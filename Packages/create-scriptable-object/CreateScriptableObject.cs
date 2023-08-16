using UnityEngine;
using UnityEditor;
using System;

public static class CreateScriptableObject
{
    [MenuItem("Assets/Create/CREATE SCRIPTABLE OBJECT ♥", priority = -5)]
    public static void Create()
    {
        MonoScript monoScript = (Selection.activeObject as MonoScript);
        if (null == monoScript)
        {
            Debug.LogError("is not a valid monoscript");
            return;
        }

        Type scriptType = monoScript.GetClass();
        if (!scriptType.IsSubclassOf(typeof(ScriptableObject)))
        {
            Debug.LogError("not a valid ScriptableObject");
            return;
        }

        string selectedPath = AssetDatabase.GetAssetPath(monoScript);
        int indexOfLastSlash = selectedPath.LastIndexOf('/');
        selectedPath = selectedPath.Substring(0, indexOfLastSlash);

        Debug.Log(selectedPath);

        ScriptableObject assetInstance = ScriptableObject.CreateInstance(scriptType);
        var path = AssetDatabase.GenerateUniqueAssetPath($"{selectedPath}/NEW_{scriptType.Name}.asset");
        AssetDatabase.CreateAsset(assetInstance, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();

        EditorGUIUtility.PingObject(assetInstance);
        Selection.activeObject = assetInstance;

    }
}
