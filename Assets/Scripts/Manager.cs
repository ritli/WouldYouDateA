using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    paused, explore, map, dialogue, choice, mainmenu
}

public class Manager : MonoBehaviour {

    bool m_mapOpen = false;

    public GameState m_state;
    GameState m_pendingGameState;
    static Manager m_instance;

    MapHandler m_map;
    ManHandler m_characters;
    LocationHandler m_location;
    ArrowHandler m_arrows;
    DialogueHandler m_dialogue;
    MainMenuHandler m_menuhandler;
    private MusicManager m_musicManager;

    GameObject m_mapButton;

    BackgroundHandler m_background;
    Image m_fadeImage;

    bool m_sceneChangeState = false;

    MapData m_currentMapData;

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
            ChangeState(GameState.mainmenu);


        }
    
    }
	
    public static GameState GameState
    {
        get
        {
            return m_instance.m_state;
        }
    }

    void InitComponents()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        m_map = GetComponentInChildren<MapHandler>();
        m_background = GetComponentInChildren<BackgroundHandler>();
        m_location = GetComponentInChildren<LocationHandler>();
        m_characters = GetComponentInChildren<ManHandler>();
        m_arrows = GetComponentInChildren<ArrowHandler>();
        m_dialogue = GetComponentInChildren<DialogueHandler>();
        m_menuhandler = GetComponentInChildren<MainMenuHandler>();
        m_musicManager = GetComponent<MusicManager>();
        m_fadeImage = GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();
        m_mapButton = transform.Find("MapButtonBG").gameObject;
    }

    public static void SetMapData(MapData mapdata)
    {
        m_instance.m_currentMapData = mapdata;

        m_instance.GetComponent<Canvas>().worldCamera = Camera.main;

        m_instance.m_location.SetLocationText(mapdata.locationName);
        m_instance.m_background.SetBackground(mapdata.background);
        m_instance.m_arrows.UpdateArrows(mapdata.arrowLocations);

        foreach(GameObject g in mapdata.characters)
        {
            m_instance.m_characters.AddCharacter(g);
        }

    }

    public static void PlayFromMenu()
    {
        m_instance.m_sceneChangeState = true;
        m_instance.m_pendingGameState = GameState.explore;
        ChangeScene("VillaGrut1924");
    }

    public void ChangeState(GameState state)
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
            case GameState.mainmenu:
                m_menuhandler.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        bool inGame;

        switch (state)
        {
            case GameState.paused:
                break;
            case GameState.explore:
                inGame = true;

                m_map.gameObject.SetActive(inGame);
                m_characters.gameObject.SetActive(inGame);
                m_location.gameObject.SetActive(inGame);
                m_dialogue.gameObject.SetActive(inGame);
                m_background.gameObject.SetActive(inGame);
                m_mapButton.gameObject.SetActive(inGame);

                break;
            case GameState.map:
                break;
            case GameState.dialogue:
                break;
            case GameState.choice:
                break;
            case GameState.mainmenu:
                inGame = false;

                m_map.gameObject.SetActive(inGame);
                m_characters.gameObject.SetActive(inGame);
                m_location.gameObject.SetActive(inGame);
                m_dialogue.gameObject.SetActive(inGame);
                m_background.gameObject.SetActive(inGame);
                m_mapButton.gameObject.SetActive(inGame);

                m_menuhandler.gameObject.SetActive(!inGame);

                break;
            default:
                break;
        }

        m_state = state;

    }

    void Update()
    {
        switch (m_state)
        {
            case GameState.paused:
                break;
            case GameState.explore:
                InputUpdate();

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

    }

    void InputUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenMap();
        }
    }

    public void OpenMap()
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

        if (m_sceneChangeState)
        {
            ChangeState(m_pendingGameState);
            m_sceneChangeState = false;
        }

        m_instance.m_characters.RemoveAllCharacters();
        m_map.InstantClose();
        m_mapOpen = false;

        SceneManager.LoadScene(name);        

        yield return new WaitForSeconds(0.05f);

        bool changeMusic = !m_currentMapData.soundtrack.name.Equals(m_musicManager.m_audioClip.name);

        if (changeMusic)
        {
            StartCoroutine(m_musicManager.FadeMusic(0.5f, 0));
        }

        while (alpha > 0)
        {
            alpha -= Time.deltaTime;

            m_fadeImage.color = new Color(c.r, c.g, c.b, alpha);

            if (changeMusic && alpha > 0.5f)
            {
                changeMusic = false;
                m_musicManager.SetAudioClip(m_currentMapData.soundtrack);
                StartCoroutine(m_musicManager.FadeMusic(0.5f, 1));
            }

            yield return new WaitForEndOfFrame();
        }



    }

}
