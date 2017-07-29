using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "ChoiceTemplate", menuName = "Dialogue/Choice", order = 1)]
public class ChoiceTemplate : ScriptableObject
{
    public string objectName = "Choices";
    public string[] buttonNames;
}
