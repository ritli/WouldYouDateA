using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationHandler : MonoBehaviour {

    [SerializeField] private Button m_buttoneTemplate;
    private BaseConvoNode m_conversation;
    private string m_name;

    public void StartConversation(BaseConvoNode[] possibleConvos, int intimacy, string name)
    {
        m_name = name;
        ClearChildren();

        List<BaseConvoNode> viable = new List<BaseConvoNode>();
        foreach (BaseConvoNode node in possibleConvos)
        {
            if (!node.Passed && intimacy >= node.IntimacyReq)
                viable.Add(node);
        }

        BaseConvoNode m_conversation = viable[Random.Range(0, viable.Count - 1)];

        //GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<DialogueHandler>().PrintText(m_name, m_conversation.VoiceLine);

        generateButtons();
    }

    public void ContinueConversation(int answer)
    {
        ClearChildren();

        if (answer == -1)
        {
            // Conversation ended
            return;
        }
         
        m_conversation.Answer(answer);

        //GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<DialogueHandler>().PrintText(m_name, m_conversation.VoiceLine);

        generateButtons();
    }

    private void generateButtons()
    {
        if (m_conversation.Choices.Length == 0)
        {
            Button btn = Button.Instantiate(m_buttoneTemplate, transform);
            btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Leave";
            //btn.GetComponent<ChoiceButton>().getData(this, -1);
        }
        else
        {
            for (int i = 0; i < m_conversation.Choices.Length; i++)
            {
                Button btn = Button.Instantiate(m_buttoneTemplate, transform);
                btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = m_conversation.Choices[i];
                //btn.GetComponent<ChoiceButton>().getData(this, i);
            }
        }
    }

    private void ClearChildren()
    {
        foreach (Transform child in transform)
            Destroy(child);
    }
}
