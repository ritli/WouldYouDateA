using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConvoNode
{
    [SerializeField] protected string m_voiceLine;
    public string VoiceLine
    {
        get{return m_voiceLine;}
    }
    [SerializeField] protected string[] m_choices;
    public string[] Choices
    {
        get{ return m_choices; }
    }
    [SerializeField] protected int[] m_correct;
    public int[] CorrectAnswers
    {
        get{ return m_correct; }
    }
    [SerializeField] protected int[] m_neutral;
    public int[] NeutralAnswers
    {
        get{ return m_neutral; }
    }
    [SerializeField] protected int[] m_negative;
    public int[] NegativeAnswers
    {
        get{ return m_negative; }
    }
    [SerializeField] protected ConvoNode m_positiveResponse = null;
    public ConvoNode PositiveResponse
    {
        get{ return m_positiveResponse; }
    }
    [SerializeField] protected ConvoNode m_neutralResponse = null;
    public ConvoNode NeutralResponse
    {
        get{ return m_neutralResponse; }
    }
    [SerializeField] protected ConvoNode m_negativeResponse = null;
    public ConvoNode NegativeResponse
    {
        get{ return m_negativeResponse; }
    }

}