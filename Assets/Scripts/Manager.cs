using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameState
{
    paused, explore, map, dialogue, choice
}

public class Manager : MonoBehaviour {

    GameState m_state;
    Manager m_instance;

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
        }
    
    }
	
    void Update()
    {

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

        }
    }

    public static void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
