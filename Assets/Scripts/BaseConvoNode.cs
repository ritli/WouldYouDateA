using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class BaseConvoNode : ConvoNode
{
    [SerializeField] private int m_regIntimacy;
    public int IntimacyReq
    {
        get{ return m_regIntimacy; }
    }
    [SerializeField] private bool m_passed;
    public bool Passed
    {
        get{ return m_passed; }
    }

    void Start()
    {
        activeNode = this;
    }

    private ConvoNode activeNode;
    public string VoiceLine
    {
        get{ return activeNode.VoiceLine; }
    }
    public string[] Choices
    {
        get{ return activeNode.Choices; }
    }



    public void Response(int answer)
    {
        if (activeNode.CorrectAnswers.Contains(answer))
            activeNode = activeNode.PositiveResponse;
        else if (activeNode.NeutralAnswers.Contains(answer))
            activeNode = activeNode.NeutralResponse;
        else if (activeNode.NegativeAnswers.Contains(answer))
            activeNode = activeNode.NegativeResponse;
        else
        {
            Debug.Log("Error: Impossible answer given.");
        }
    }
}
