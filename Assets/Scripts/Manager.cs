using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum GameState
{
    paused, explore, map, dialogue, choice
}

public class Manager : MonoBehaviour {

    bool m_mapOpen = false;

    GameState m_state;
    static Manager m_instance;

    MapHandler m_map;
    LocationHandler m_location;

    BackgroundHandler m_background;
    Image m_fadeImage;

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
        m_background = GetComponentInChildren<BackgroundHandler>();
        m_location = GetComponentInChildren<LocationHandler>();
        m_fadeImage = GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();
    }

    public static void SetMapData(Sprite background, string location)
    {
        m_instance.GetComponent<Canvas>().worldCamera = Camera.main;

        m_instance.m_location.SetLocationText(location);
        m_instance.m_background.SetBackground(background);
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
        m_instance.StartSceneChange(name);
    }

    void StartSceneChange(string name)
    {
        StartCoroutine(ChangeSceneRoutine(name));
    }

    IEnumerator ChangeSceneRoutine(string name)
    {
        Color c = m_fadeImage.color;
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * 2;

            m_fadeImage.color = new Color(c.r, c.g, c.b, alpha);

            yield return new WaitForEndOfFrame();
        }

        m_map.InstantClose();
        m_mapOpen = false;

        SceneManager.LoadScene(name);

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 2;

            m_fadeImage.color = new Color(c.r, c.g, c.b, alpha);

            yield return new WaitForEndOfFrame();
        }

    }

}
