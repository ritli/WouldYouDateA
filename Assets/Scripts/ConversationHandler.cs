using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationHandler : MonoBehaviour {

    [SerializeField] private Button m_buttoneTemplate;
    private BaseConvoNode m_conversation;

    public void StartConversation(BaseConvoNode[] possibleConvos, int intimacy)
    {
        List<BaseConvoNode> viable = new List<BaseConvoNode>();
        foreach (BaseConvoNode node in possibleConvos)
        {
            if (!node.Passed && intimacy >= node.IntimacyReq)
                viable.Add(node);
        }

        BaseConvoNode m_conversation = viable[Random.Range(0, viable.Count - 1)];

        foreach (string btnName in m_conversation.Choices)
            Instantiate(m_buttoneTemplate, transform).GetComponentInChildren<Text>().text = btnName;
    }

    public void ContinueConversation(int answer)
    {
        ClearChildren();
    }

    private void ClearChildren()
    {
        foreach (Transform child in transform)
            Destroy(child);
    }
}
