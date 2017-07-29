using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject
{
    [MenuItem("Assets/Create/My Scriptable Object")]
    public static void CreateMyAsset()
    {
        ChoiceTemplate asset = ScriptableObject.CreateInstance<ChoiceTemplate>();

        AssetDatabase.CreateAsset(asset, "Assets/ChoiceTemplate.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
