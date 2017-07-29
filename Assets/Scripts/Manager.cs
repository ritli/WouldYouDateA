using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameState
{
    paused, explore, map, dialogue, choice
}

public class Manager : MonoBehaviour {

    bool m_mapOpen = false;

    GameState m_state;
    Manager m_instance;

    MapHandler m_map;

    [SerializeField] private CharacterData[] m_charData;

	void Start () {
        if (FindObjectsOfType<Manager>().Length != 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            m_instance = this;

            InitComponents();
        }
    
    }
	
    void InitComponents()
    {
        m_map = GetComponentInChildren<MapHandler>();
    }

    void Update()
    {
        InputUpdate();

        switch (m_state)
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
            default:
                break;
        }

    }

    void InputUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!m_mapOpen)
            {
                m_mapOpen = true;
                m_map.Open();
            }
            else
            {
                m_mapOpen = false;
                m_map.Close();
            }
        }
    }

    public static void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
