using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoiceNode
{
    public string voiceLine;
    public string[] buttonNames;
    public int correctChoice;
    public float intimacyGain;
    public int reqIntimacy;
    public ChoiceNode prereqChoice = null;
    public string followupPositive, followupNegative;
    public bool passed;
}
