using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Character : MonoBehaviour, IPointerDownHandler {

    [SerializeField]
    Vector2 offset = Vector2.zero;
    public CharacterData characterData;

    bool m_hasLeft = true;

    public Day[] m_daysToAppear = new Day[7];

	void Start () 
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);

        foreach(Day day in m_daysToAppear)
        {
            if (day.Equals(Manager.GetDay()) && !Manager.GetCharacterLeft(characterData.Type))
            {
                Appear();
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

        while(timeElapsed < time)
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
}
