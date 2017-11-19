using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Mood
{
    neutral,
    love,
    angry
}


public class Character : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    Vector2 offset = Vector2.zero;
    public CharacterData characterData;

    bool m_hasLeft = true;
    bool m_InDialogue = false;


    [Header("Mood Sprites")]
    public Sprite m_neutral;
    public Sprite m_love;
    public Sprite m_angry;

    public Day[] m_daysToAppear = new Day[7];

    Mood currentMood;

	void Start () 
    {
        GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 0);

        if (!Manager.GetCharacterAngry(characterData.Type))
        {
            foreach (Day day in m_daysToAppear)
            {
                if (day.Equals(Manager.GetDay()) && !Manager.GetCharacterLeft(characterData.Type))
                {
                    Appear();
                }
            }
        }
        
    }

    /// <summary>
    /// Data is passed by the instantiating function.
    /// </summary>
    /// <param name="data">Data.</param>
    void LoadCharData(ref CharacterData data)
    {
        characterData = data;
    }

    public Vector2 GetOffset()
    {
        return offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Manager.GameState == GameState.explore && !m_hasLeft)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, GetComponent<Image>().color.a);
            m_InDialogue = true;
            Manager.StartDialogue(characterData, this);
        }
    }

    public void Leave()
    {
        m_hasLeft = true;

        StartCoroutine(Fade(false));
    }

    public void Appear()
    {
        m_hasLeft = false;

        StartCoroutine(Fade(true));

    }

    IEnumerator Fade(bool fadeIn)
    {
        float time = 1;
        float timeElapsed = 0;
        Image image = GetComponent<Image>();

        while(timeElapsed < time && image.sprite != null)
        {
            Color c = image.color;

            if (fadeIn)
            {
                c.a = timeElapsed / (time + 0.001f);
            }
            else
            {
                c.a = 1 - timeElapsed / (time + 0.001f);
            }

            image.color = c;

            yield return new WaitForEndOfFrame();

            timeElapsed += Time.deltaTime;
        }
    }

    public void SetMood(Mood mood)
    {
        Image renderer = GetComponent<Image>();

        switch (mood)
        {
            case Mood.neutral:
                if (m_neutral)
                {
                    renderer.sprite = m_neutral;
                }
                break;
            case Mood.love:
                if (m_love)
                {
                    renderer.sprite = m_love;
                }
                else if (m_neutral)
                {
                    renderer.sprite = m_neutral;
                }
                break;
            case Mood.angry:
                if (m_angry)
                {
                    renderer.sprite = m_angry;
                }
                else if (m_neutral)
                {
                    renderer.sprite = m_neutral;
                }
                break;
            default:
                if (m_neutral)
                {
                    renderer.sprite = m_neutral;
                }
                break;
        }

        currentMood = mood;
    }

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (!m_InDialogue)
         GetComponent<Image>().color = new Color(1, 1, 1, GetComponent<Image>().color.a);

    }

    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (!m_InDialogue)
            GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, GetComponent<Image>().color.a);
        
    }
}
