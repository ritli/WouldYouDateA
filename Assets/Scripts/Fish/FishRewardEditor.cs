using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FishReward))]
[CanEditMultipleObjects]
public class FishRewardEditor : Editor {

    SerializedProperty item;

    int currentPos = 1;

    void OnEnable()
    {
        item = serializedObject.FindProperty("item");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayoutOption[] options = {  GUILayout.Height(25), GUILayout.MinWidth(25), GUILayout.MaxWidth(45) };

        currentPos = EditorGUILayout.IntSlider(currentPos, 0, 10);

        item.arraySize = currentPos * currentPos;

        for (int y = 1; y < currentPos + 1; y++)
        {



            //
            EditorGUILayout.BeginHorizontal();

            for (int i = 0; i < currentPos; i++)
            {
                EditorGUILayout.ObjectField(item.GetArrayElementAtIndex(i * y), GUIContent.none, options);
            }

            EditorGUILayout.EndHorizontal();
        }



        serializedObject.ApplyModifiedProperties();

    }
}
