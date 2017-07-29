using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButton : MonoBehaviour 
{
    private ConversationHandler m_parent;
    private int m_choiceId;

    public void getData(ConversationHandler parent, int choiceId)
    {
        m_parent = parent;
        m_choiceId = choiceId;
    }

    public void Clicked()
    {
        m_parent.ContinueConversation(m_choiceId);
    }
}
