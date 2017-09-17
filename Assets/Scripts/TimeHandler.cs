using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Day
{
    Mon, Tue, Wed, Thu, Fri, Sat, Sun
}

public class TimeHandler : MonoBehaviour {

    TMPro.TextMeshProUGUI m_Text;
    public Day m_currentDay = Day.Mon;

    public int m_Hours = 5;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            IncrementTime(12);
        }
    }
    private void Start()
    {
        m_Text = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        UpdateText();
    }

    void UpdateText()
    {
        m_Text.text = m_Hours.ToString("00") + ":00 " + m_currentDay.ToString();
    }

    public void IncrementTime(int amount)
    {
        m_Hours += amount;

        if (m_Hours > 23)
        {
            m_currentDay =  (Day)(((int)m_currentDay + 1) % 7);
            m_Hours -= 24;

            Manager.ResetCharacterLeave();
        }

        UpdateText();
    }

}
