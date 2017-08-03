using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManHandler : MonoBehaviour {

    public GameObject testCharacter;

    public List<GameObject> m_characters;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //AddCharacter(Instantiate(testCharacter, transform));
        }
    }

	void Start () {

        if (transform.childCount != 0)
        {
            m_characters.AddRange(GetComponentsInChildren<GameObject>());

            PositionCharacters(Manager.GameState);
        }
    }

    void PositionCharacters(GameState state)
    {
        float width = Screen.width;
        int count = m_characters.Count;
        float xOffset = 2;

        switch (state)
        {
            case GameState.paused:
                break;
            case GameState.explore:
                break;
            case GameState.map:
                break;
            case GameState.dialogue:
                break;
            case GameState.choice:
                break;
            case GameState.mainmenu:
                break;
            default:
                break;
        }


        for (int i = 0; i < count; i++)
        {
            Vector2 offset = m_characters[i].GetComponent<Character>().GetOffset();

            m_characters[i].transform.position = new Vector3(xOffset * i, transform.position.y - 1) + (Vector3)offset;
        }


/*
        if (count != 1)
        {
            for (int i = 0; i < count; i++)
            {
                m_characters[i].transform.position = new Vector2((width / 5) * i + width / count, m_characters[i].transform.position.y);
            }
        }
        else
        {
            m_characters[0].transform.position = new Vector2((width / 2), m_characters[0].transform.position.y);
        }
 */       
    }

    public void AddCharacter(GameObject character)
    {
        if (m_characters.Count < 5)
        {
            m_characters.Add(Instantiate(character, transform.position, transform.rotation, transform));
            PositionCharacters(Manager.GameState);
        }

    }

    public void RemoveCharacter(int index)
    {
        m_characters.RemoveAt(0);
    }

    public void RemoveAllCharacters()
    {
        foreach(GameObject g in m_characters)
        {
            Destroy(m_characters[0]);
        }

        m_characters.Clear();
    }
}
