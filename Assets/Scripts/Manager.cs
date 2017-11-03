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

    InGameMenuHandler m_ingameMenu;
    ChoiceHandler m_choiceHandler;
    MapHandler m_map;
    ManHandler m_characters;
    LocationHandler m_location;
    ArrowHandler m_arrows;
    DialogueHandler m_dialogue;
    MainMenuHandler m_menuhandler;
    TimeHandler m_timeHandler;
    SaveLoadHandler m_saveHandler;
    private MusicManager m_musicManager;

    GameObject m_mapButton;

    Character currentCharacter;

    BackgroundHandler m_background;
    Image m_fadeImage;

    bool m_sceneChangeState = false;
    bool m_eventTriggeredThroughDialogue;
    Dialogue m_currentDialogue;
    CharacterData m_currentCharacter;

    ProgressManager m_progress;

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

        m_ingameMenu = GetComponentInChildren<InGameMenuHandler>();
        m_saveHandler = GetComponentInChildren<SaveLoadHandler>();
        m_choiceHandler = GetComponentInChildren<ChoiceHandler>();
        m_map = GetComponentInChildren<MapHandler>();
        m_background = GetComponentInChildren<BackgroundHandler>();
        m_location = GetComponentInChildren<LocationHandler>();
        m_characters = GetComponentInChildren<ManHandler>();
        m_arrows = GetComponentInChildren<ArrowHandler>();
        m_timeHandler = GetComponentInChildren<TimeHandler>();
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
        m_instance.m_arrows.UpdateArrows(mapdata.arrowLocations, false);

        GameObject buttonHolder = m_instance.transform.Find("CustomButtons").gameObject;

        for (int i = buttonHolder.transform.childCount; i > 0; i--)
        {
            Destroy(buttonHolder.transform.GetChild(i-1).gameObject);
        }

        foreach (GameObject g in mapdata.customButtons)
        {
            GameObject button = Instantiate(g, buttonHolder.transform);
            button.GetComponent<Animator>().Play("Open");
        }

        foreach(GameObject g in mapdata.characters)
        {
            m_instance.m_characters.AddCharacter(g);
        }
    }

    public static void PlayFromMenu()
    {
        m_instance.m_sceneChangeState = true;
        m_instance.m_pendingGameState = GameState.explore;

        m_instance.m_saveHandler.StartNewGame();
    }

    public static void PlayFromMenuLoad()
    {
        m_instance.m_sceneChangeState = true;
        m_instance.m_pendingGameState = GameState.explore;

        ChangeScene(ProgressManager.current.currentScene);
    }

    public static void SetGameState(GameState state)
    {
        m_instance.ChangeState(state);
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
                inGame = false;

                if (m_mapOpen)
                {
                    m_map.Close();
                }
                m_mapButton.GetComponentInChildren<Button>().interactable = false;
                m_arrows.gameObject.SetActive(inGame);

                break;
            case GameState.explore:
                inGame = true;

                m_ingameMenu.gameObject.SetActive(inGame);
                m_choiceHandler.gameObject.SetActive(inGame);
                m_timeHandler.gameObject.SetActive(inGame);

                if (m_state.Equals(GameState.dialogue) || m_state.Equals(GameState.choice))
                {
                    m_arrows.SetActive(inGame, true);
                }
                else
                {
                    m_arrows.gameObject.SetActive(true);
                }

                m_map.gameObject.SetActive(inGame);
                m_characters.gameObject.SetActive(inGame);
                m_location.gameObject.SetActive(inGame);
                m_dialogue.gameObject.SetActive(inGame);
                m_background.gameObject.SetActive(inGame);
                m_mapButton.gameObject.SetActive(inGame);
                m_mapButton.GetComponentInChildren<Button>().interactable = true && !m_instance.m_currentMapData.blockTravel;

                m_menuhandler.gameObject.SetActive(!inGame);

                break;
            case GameState.map:
                break;
            case GameState.dialogue:
                m_arrows.gameObject.SetActive(false);
                m_mapButton.GetComponentInChildren<Button>().interactable = false;
                break;
            case GameState.choice:
                m_choiceHandler.gameObject.SetActive(true);
                break;
            case GameState.mainmenu:
                inGame = false;

                m_ingameMenu.gameObject.SetActive(inGame);
                m_timeHandler.gameObject.SetActive(inGame);
                m_map.gameObject.SetActive(inGame);
                m_characters.gameObject.SetActive(inGame);
                m_location.gameObject.SetActive(inGame);
                m_dialogue.gameObject.SetActive(inGame);
                m_background.gameObject.SetActive(inGame);
                m_mapButton.gameObject.SetActive(inGame);
                m_arrows.gameObject.SetActive(inGame);

                m_menuhandler.gameObject.SetActive(!inGame);
                m_saveHandler.Init();
                m_menuhandler.Init();

                break;
            default:
                break;
        }

        m_state = state;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ChangeScene("VillaGrut");
            ChangeState(GameState.explore);
        }


        switch (m_state)
        {
            case GameState.paused:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (GameState == GameState.paused)
                    {
                        ChangeState(GameState.explore);
                    }
                    else if (GameState == GameState.explore)
                    {
                        ChangeState(GameState.paused);
                    }

                    m_ingameMenu.ToggleMenu();
                }
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

    public static void StartDialogue(CharacterData characterData, Character currentCharacter)
    {
        if (m_instance.m_mapOpen)
        {
            m_instance.m_map.Close();
        }

        m_instance.currentCharacter = currentCharacter;

        DialogueContainer container = DialogueContainer.Load(characterData.Type);

        Dialogue dialogue = null;
        bool hasChoices = false;
        int i = 0;

        foreach(Dialogue d in container.m_dialogues)
        {
            if (i == ProgressManager.current.GetProgress((int)characterData.Type))
            {
                dialogue = d;

                if(d.choices.Count > 0)
                {
                    hasChoices = true;
                }
            }

            i++;
        }

        currentCharacter.SetMood(dialogue.Mood);

        if (dialogue.text == null && !hasChoices)
        {
            if (!dialogue.Event.Equals("NoEvent"))
            {
                GameObject g = Resources.Load<GameObject>(dialogue.Event);

                StartEvent(g.GetComponent<GameEvent>(), g.GetComponent<GameEvent>().m_nextEvent);

                print("InstaEvent Started");

                if (dialogue.ContinueDialogue)
                {
                    ProgressManager.current.AddProgress((int)characterData.Type, 1);
                }
            }
        }
        else
        {
            m_instance.ChangeState(GameState.dialogue);

            m_instance.m_dialogue.PrintText(dialogue, characterData, hasChoices);
        }
    }

    public static void EndDialogue(Dialogue dialogue, bool hasChoices, CharacterData character)
    {
        if (hasChoices)
        {
            m_instance.StartChoice(dialogue, character);
        }
        else if(dialogue.ContinueDialogue)
        {
            ProgressManager.current.AddProgress((int)character.Type, 1);

            if (dialogue.LeaveDialogue)
            {
                m_instance.currentCharacter.Leave();
                m_instance.m_timeHandler.IncrementTime(6);
                m_instance.ChangeState(GameState.explore);
                m_instance.m_dialogue.Close();
                ProgressManager.current.SetCharacterLeft((int)character.Type);

                if (!dialogue.Event.Equals("NoEvent"))
                {
                    GameObject g = Resources.Load<GameObject>(dialogue.Event);

                    print("PostDialogue event started");

                    StartEvent(g.GetComponent<GameEvent>(), g.GetComponent<GameEvent>().m_nextEvent);
                }
                else
                {
                    m_instance.StartFade(true, 1f, 0f);
                    m_instance.StartFade(false, 1f, 1.1f);
                }
            }
            else
            {
                StartDialogue(character, m_instance.currentCharacter);
            }
            return;
        }
        else
        {
            m_instance.ChangeState(GameState.explore);
            m_instance.m_dialogue.Close();
        }

    }

    void StartChoice(Dialogue dialogue, CharacterData character)
    {
        m_instance.ChangeState(GameState.choice);
        m_choiceHandler.StartChoiceEvent(dialogue, character);
    }

    public static void EndChoice(ChoiceType choiceResult, CharacterData character, Dialogue dialogue, int choiceIndex)
    {
        bool badChoice = false;
        int valueChange = 0;

        switch (choiceResult)
        {
            case ChoiceType.good:
                valueChange = 1;
                m_instance.currentCharacter.SetMood(Mood.love);

                break;
            case ChoiceType.neutral:
                m_instance.currentCharacter.SetMood(Mood.angry);

                break;
            case ChoiceType.bad:
                valueChange = -1;
                m_instance.currentCharacter.SetMood(Mood.angry);
                ProgressManager.current.SetCharacterAngry((int)character.Type);
                badChoice = true;

                break;
            default:
                break;
        }

        ProgressManager.current.AddProgress((int)character.Type, valueChange);

        if (dialogue.response.Count > 0)
        {
            m_instance.m_dialogue.PrintResponse(dialogue.response[choiceIndex], character, dialogue, badChoice);
        }
        else
        {
            EndResponse(dialogue, character, badChoice);
        }
    }

    public static void EndResponse(Dialogue dialogue, CharacterData character, bool badChoice)
    {
        if (badChoice)
        {
            m_instance.m_dialogue.Close();
            m_instance.ChangeState(GameState.explore);

            m_instance.m_timeHandler.IncrementTime(6);
            m_instance.currentCharacter.Leave();
            ProgressManager.current.SetCharacterLeft((int)character.Type);

            m_instance.StartFade(true, 1f, 0f);
            m_instance.StartFade(false, 1f, 1.1f);

            return;
        }

        if (dialogue.ContinueDialogue)
        {
            StartDialogue(character, m_instance.currentCharacter);
            m_instance.ChangeState(GameState.dialogue);
        }
        else
        {
            m_instance.m_dialogue.Close();
            m_instance.ChangeState(GameState.explore);

            if (dialogue.LeaveDialogue)
            {
                m_instance.m_timeHandler.IncrementTime(6);
                m_instance.currentCharacter.Leave();
                ProgressManager.current.SetCharacterLeft((int)character.Type);

                print("Event : " + dialogue.Event);

                if (!dialogue.Event.Equals("NoEvent"))
                {
                    GameObject g = Resources.Load<GameObject>(dialogue.Event);

                    StartEvent(g.GetComponent<GameEvent>(), g.GetComponent<GameEvent>().m_nextEvent);
                    return;
                }

                m_instance.StartFade(true, 1f, 0f);
                m_instance.StartFade(false, 1f, 1.1f);
            }
        }
    }

    public static void StartEvent(GameEvent currentEvent, GameEvent nextEvent)
    {
        print("Event started");

        switch (currentEvent.m_type)
        {
            case GameEventType.ShowText:
                m_instance.ChangeState(GameState.dialogue);
                m_instance.m_dialogue.PrintTextEvent(currentEvent.GetText, currentEvent.GetName, nextEvent);
                break;
            case GameEventType.Teleport:
                ChangeScene(currentEvent.GetScene);

                if (nextEvent)
                {
                    StartEvent(nextEvent, nextEvent.m_nextEvent);
                }
                else
                {
                    m_instance.m_dialogue.Close();
                    m_instance.ChangeState(GameState.explore);
                }
                break;
            case GameEventType.StartDialogue:
                foreach (Character c in m_instance.m_characters.GetComponentsInChildren<Character>())
                {
                    if (c.characterData.Type == currentEvent.GetCharacter)
                    {
                        StartDialogue(c.characterData, c);
                        break;
                    }
                }
                break;
            default:
                break; 
        }
    }

    public static void EndEvent(GameEvent nextEvent)
    {
        if (nextEvent)
        {
            if (nextEvent.m_type.Equals(GameEventType.Teleport))
            {
                m_instance.ChangeState(GameState.explore);
                m_instance.m_dialogue.Close();
            }
            StartEvent(nextEvent, nextEvent.m_nextEvent);
        }

        else
        {
            m_instance.m_dialogue.Close();
            m_instance.ChangeState(GameState.explore);
        }
    }

    public static Day GetDay()
    {
        return m_instance.m_timeHandler.m_currentDay;
    }

    public static string GetTime()
    {
        return m_instance.m_timeHandler.m_Hours.ToString("00") + ":00 ";
    }

    public static void EndDay()
    {
        m_instance.m_timeHandler.m_Hours = 8;
        m_instance.m_timeHandler.IncrementTime(24);

        m_instance.StartFade(true, 0.3f, 0f);
        m_instance.StartFade(false, 0.3f, 0.4f);
    }

    public static void ResetCharacterLeave()
    {
        ProgressManager.current.ResetLeave();
    }

    public static bool GetCharacterLeft(Characters type)
    {
        return ProgressManager.current.GetCharacterLeft((int)type);
    }

    public static bool GetCharacterAngry(Characters type)
    {
        return ProgressManager.current.GetCharacterAngry((int)type);
    }

    void InputUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M) && !m_currentMapData.blockTravel)
        {
            OpenMap();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameState == GameState.paused)
            {
                ChangeState(GameState.explore);
            }
            else if (GameState == GameState.explore)
            {
                ChangeState(GameState.paused);
            }

            m_ingameMenu.ToggleMenu();
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

    void StartFade(bool fadeOut, float time, float delay)
    {
        StartCoroutine(Fade(fadeOut, time, delay));
    }

    IEnumerator Fade(bool fadeOut, float time, float delay)
    {
        yield return new WaitForSeconds(delay);

        Color c = m_fadeImage.color;
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime / time;

            if (!fadeOut)
            {
                m_fadeImage.color = new Color(c.r, c.g, c.b, 1- alpha);
            }
            else
            {
                m_fadeImage.color = new Color(c.r, c.g, c.b, alpha);

            }

            yield return new WaitForEndOfFrame();
        }
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

        if (name == "Menu")
        {
            ChangeState(GameState.mainmenu);
        }

        if (m_instance.m_timeHandler)
        {
            m_instance.m_timeHandler.IncrementTime(1);
        }

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

    static void SaveGame()
    {
        ProgressManager.current = ProgressManager.current;
        SaveLoad.Save();
    }

    static void LoadGame()
    {

    }

    public static void Quit()
    {
        Application.Quit();
    }
}
