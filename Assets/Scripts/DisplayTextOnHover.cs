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

    Vector3 position;

    Image m_textImage;
    TMPro.TextMeshProUGUI m_textText;

    float alpha = 0;
    bool m_mouseover = false; 


    void Update()
    {
        DisplayText(m_mouseover);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_mouseover = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_mouseover = false;
    }

    public void DestroyTextInstance()
    {
        Destroy(m_textInstance);
    }

    void DisplayText(bool visible)
    {
        int mult = -1;

        if (!m_textInstance && visible)
        {
            m_textInstance = Instantiate(m_textObject, transform);

            m_textInstance.transform.position += (Vector3)m_offset;

            m_textInstance.transform.parent = transform;

            position = m_textInstance.transform.position;

            m_textText = m_textInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            m_textImage = m_textInstance.GetComponent<Image>();

            m_textText.text = m_textToDisplay;
        }
        else if (alpha == 0 && !visible && m_textInstance)
        {
            Destroy(m_textInstance);
        }

        if (visible)
        {
            mult = 1;
        }

        alpha += Time.deltaTime * mult * 3;
        alpha = Mathf.Clamp01(alpha);

        if (m_textInstance)
        {
            m_textInstance.transform.position = position;

        Color c = m_textText.color;
        m_textText.color = new Color(c.r, c.g, c.b, alpha);
        c = m_textImage.color;
        m_textImage.color = new Color(c.r, c.g, c.b, alpha);

        }

    }

    public void HideText()
    {
        m_mouseover = false;
        Destroy(m_textInstance);
    }
}
