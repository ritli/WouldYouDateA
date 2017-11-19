
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(FishReward))]
[CanEditMultipleObjects]
public class FishRewardEditor : Editor {

    SerializedProperty item;
    SerializedProperty size;

    int currentPos;

    void OnEnable()
    {
        item = serializedObject.FindProperty("item");
        size = serializedObject.FindProperty("size");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        currentPos = size.intValue;

        GUILayoutOption[] options = {  GUILayout.MinHeight(25), GUILayout.MinHeight(35), GUILayout.MinWidth(25), GUILayout.MaxWidth(150)};  

        currentPos = EditorGUILayout.IntSlider(currentPos, 0, 10);
        
        if (item.arraySize != currentPos * currentPos)
        {
            item.arraySize = currentPos * currentPos;
        }

        for (int y = 1; y < currentPos + 1; y++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int i = 0; i < currentPos; i++)
            {
                item.GetArrayElementAtIndex(i * y).objectReferenceValue = EditorGUILayout.ObjectField(item.GetArrayElementAtIndex(i * y).objectReferenceValue, typeof(GameObject), true, options);
            }

            EditorGUILayout.EndHorizontal();
        }

        size.intValue = currentPos;

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
