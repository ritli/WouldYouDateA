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

    Image m_textImage;
    Text m_textText;

    float alpha = 0;
    bool m_mouseover = false; 

    void Update()
    {
        DisplayText(m_mouseover);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_mouseover = true;
        print("Enter");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Exit");

        m_mouseover = false;
    }

    void DisplayText(bool visible)
    {

        int mult = -1;
  
        if (!m_textInstance)
        {
            m_textInstance = Instantiate(m_textObject, transform);

            m_textText = m_textInstance.GetComponentInChildren<Text>();
            m_textImage = m_textInstance.GetComponent<Image>();

            m_textText.text = m_textToDisplay;
        }

        if (visible)
        {
            mult = 1;
        }

        alpha += Time.deltaTime * mult * 3;
        alpha = Mathf.Clamp01(alpha);

        Color c = m_textText.color;
        m_textText.color = new Color(c.r, c.g, c.b, alpha);
        c = m_textImage.color;
        m_textImage.color = new Color(c.r, c.g, c.b, alpha);
    }
}
