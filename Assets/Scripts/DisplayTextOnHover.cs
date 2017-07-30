using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayTextOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string m_textToDisplay;
    public Vector2 m_offset;
    public GameObject m_textObject;
    GameObject m_textInstance;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayText(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisplayText(false);
    }

    void DisplayText(bool visible)
    {
        if (visible)
        {
            if (!m_textInstance)
            {
                m_textInstance = Instantiate(m_textObject, transform);
                m_textInstance.GetComponentInChildren<Text>().text = m_textToDisplay;
            }
        }

        m_textInstance.SetActive(visible);
    }
}
