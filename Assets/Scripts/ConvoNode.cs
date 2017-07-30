using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public ConvoNode(string voiceline, string[] choices, int[] correct, int[] neutral, int[] negative, 
        ConvoNode posRes = null, ConvoNode netRes = null, ConvoNode negRes = null)
    {
        m_voiceLine = voiceline;
        m_choices = choices;
        m_correct = correct;
        m_neutral = neutral;
        m_negative = negative;
        m_positiveResponse = posRes;
        m_neutralResponse = netRes;
        m_negativeResponse = negRes;
    }

    public ConvoNode(){}

}

[System.Serializable]
public class BaseConvoNode : ConvoNode
{
    [SerializeField] private int m_reqIntimacy;
    public int IntimacyReq
    {
        get{ return m_reqIntimacy; }
    }
    [SerializeField] private bool m_passed;
    public bool Passed
    {
        get{ return m_passed; }
    }

    private ConvoNode activeNode;

    /// <summary>
    /// Set active conversation node based on answer.
    /// </summary>
    /// <param name="answer">Answer.</param>
    public void Answer(int answer)
    {
        if (activeNode.CorrectAnswers.Contains(answer))
        {
            activeNode = activeNode.PositiveResponse;

            // If conversation has ended on positive, flag as passed.
            if (activeNode.Choices.Length == 0)
                m_passed = true;
        }
        else if (activeNode.NeutralAnswers.Contains(answer))
            activeNode = activeNode.NeutralResponse;
        else if (activeNode.NegativeAnswers.Contains(answer))
            activeNode = activeNode.NegativeResponse;
        else
        {
            Debug.Log("Error: Impossible answer given.");
        }
    }

    public BaseConvoNode(int reqIntimacy, string voiceline, string[] choices, int[] correct, int[] neutral, int[] negative, 
        ConvoNode posRes = null, ConvoNode netRes = null, ConvoNode negRes = null, bool passed = false)
    {
        m_voiceLine = voiceline;
        m_choices = choices;
        m_correct = correct;
        m_neutral = neutral;
        m_negative = negative;
        m_positiveResponse = posRes;
        m_neutralResponse = netRes;
        m_negativeResponse = negRes;

        m_reqIntimacy = reqIntimacy;
        m_passed = passed;

        activeNode = this;
    }
}
