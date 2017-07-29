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
            AddCharacter(Instantiate(testCharacter, transform));
        }
    }

	void Start () {

        if (transform.childCount != 0)
        {
            m_characters.AddRange(GetComponentsInChildren<GameObject>());

            PositionCharacters();
        }
    }

    void PositionCharacters()
    {
        float width = Screen.width;
        int count = m_characters.Count;

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
        
    }

    void AddCharacter(GameObject character)
    {
        if (m_characters.Count < 5)
        {
            m_characters.Add(character);
            PositionCharacters();
        }

    }

    void RemoveCharacter(int index)
    {
        m_characters.RemoveAt(0);
    }
}
