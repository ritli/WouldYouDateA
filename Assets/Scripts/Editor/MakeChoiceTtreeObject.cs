using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeChoiceTreeObject 
{
    [MenuItem("Assets/Create/Choice tree object")]
    public static void CreateMyAsset()
    {
        ChoiceTreeTemplate asset = ScriptableObject.CreateInstance<ChoiceTreeTemplate>();

        AssetDatabase.CreateAsset(asset, "Assets/ChoiceTreeTemplate.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
