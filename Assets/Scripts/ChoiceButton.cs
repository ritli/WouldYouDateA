using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour 
{
    public ChoiceType m_buttonType;
    int m_buttonIndex;
    
    ChoiceHandler m_parent;
    Button m_button;

    public void InitButton(ChoiceType buttonType, int buttonIndex)
    {
        m_buttonType = buttonType;
        m_buttonIndex = buttonIndex;

        m_button = GetComponent<Button>();
        m_parent = transform.parent.GetComponent<ChoiceHandler>();

        switch (m_buttonType)
        {
            case ChoiceType.good:
                m_button.onClick.AddListener(() => m_parent.GoodResponse(m_buttonIndex));
                break;
            case ChoiceType.neutral:
                m_button.onClick.AddListener(() => m_parent.NeutralResponse(m_buttonIndex));
                break;
            case ChoiceType.bad:
                m_button.onClick.AddListener(() => m_parent.BadResponse(m_buttonIndex));
                break;
            default:
                break;
        }
    }
}
