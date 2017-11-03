using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDotDotAnimator : MonoBehaviour {

    TMPro.TextMeshProUGUI m_Text;
    float m_Time = 3;
    float m_TimeElapsed = 0;

    bool m_Hidden = true;

	void Start () {
        m_Text = GetComponent<TMPro.TextMeshProUGUI>();
        Hide();
	}
	
    public void Show()
    {
        if (m_Hidden)
        {
            m_Text.color = new Color(m_Text.color.r, m_Text.color.g, m_Text.color.b, 1);
            m_TimeElapsed = 0;
            m_Hidden = false;
        }

    }

    public void Hide()
    {
        if (!m_Hidden)
        {
            m_Text.color = new Color(m_Text.color.r, m_Text.color.g, m_Text.color.b, 0);
            m_TimeElapsed = 0;
            m_Hidden = true;
        }

    }

    void Update () {
        string dots = "";

        for (int i = 0; i < Mathf.CeilToInt(m_TimeElapsed); i++)
        {
            dots += ".";
        }

        m_Text.text = dots;

        m_TimeElapsed += Time.deltaTime;

        if (m_TimeElapsed > m_Time)
        {
            m_TimeElapsed = 0;
        }

    }


}
