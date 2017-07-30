using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "ChoiceTreeTemplate", menuName = "Dialogue/Choice tree", order = 2)]
public class ChoiceTreeTemplate : ScriptableObject 
{
    public string objectName = "Choice tree";
    public BaseConvoNode[] choices;
}
