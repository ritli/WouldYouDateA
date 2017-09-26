using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEventType
{
    ShowText, Teleport
}
public enum GameEventTriggerType
{
    OnEnter, OnClick, OnSequence
}

public class GameEvent : MonoBehaviour {

    public bool m_displayOnce;
    public GameEventType m_type;
    public GameEventTriggerType m_triggerType;

    public GameEvent m_nextEvent;

    bool m_init;
    bool m_triggered = false;
    GameEvent_Text m_textHolder;
    GameEvent_Teleport m_teleport;
    void Start () {

        m_init = true;

        switch (m_type)
        {
            case GameEventType.ShowText:
                m_textHolder = GetComponent<GameEvent_Text>();
                break;
            case GameEventType.Teleport:
                m_teleport = GetComponent<GameEvent_Teleport>();
                break;
            default:
                break;
        }

        switch (m_triggerType)
        {
            case GameEventTriggerType.OnEnter:
                TriggerEvent();
                break;
            case GameEventTriggerType.OnClick:
                break;
            case GameEventTriggerType.OnSequence:
                break;
            default:
                break;
        }
    }

    public string GetScene
    {
        get
        {
            if (!m_init)
            {
                Start();
            }

            if (m_teleport.m_sceneName.Length > 0)
            {
                return m_teleport.m_sceneName;
            }

            Debug.LogError("NO TEXT IN EVENT");
            return null;
        }
    }

    public string GetText
    {
        get 
        {
            if (!m_init)
            {
                Start();
            }

            if (m_textHolder.m_text.Length > 0)
            {
                return m_textHolder.m_text;
            }

            Debug.LogError("NO TEXT IN EVENT");
            return null;
        }
    }

    public string GetName
    {
        get {
            if (!m_init)
            {
                Start();
            }

            if (m_textHolder.m_name.Length > 0)
            {
                return m_textHolder.m_name;
            }

            Debug.LogError("NO NAME IN EVENT");
            return null;
        }
    }

    public void TriggerEvent()
    {
        if (!m_triggered)
        {
            if (!m_init)
            {
                Start();
            }

            m_triggered = true;

            switch (m_type)
            {
                case GameEventType.ShowText:
                    Manager.StartEvent(this, m_nextEvent);
                    break;
                case GameEventType.Teleport:
                    Manager.StartEvent(this, m_nextEvent);
                    break;
                default:
                    break;
            }
        }
    }

	void Update () {


        if (Input.GetMouseButtonDown(0) && m_type.Equals(GameEventTriggerType.OnClick))
        {
            TriggerEvent();
        }
	}

    private void OnDrawGizmosSelected()
    {
        switch (m_type)
        {
            case GameEventType.ShowText:
                if (!GetComponent<GameEvent_Text>())
                {
                    gameObject.AddComponent<GameEvent_Text>();
                }
                else if (GetComponent<GameEvent_Teleport>()) {
                    DestroyImmediate(gameObject.GetComponent<GameEvent_Teleport>());
                }
                break;
            case GameEventType.Teleport:
                if (GetComponent<GameEvent_Text>())
                {
                    DestroyImmediate(gameObject.GetComponent<GameEvent_Text>());
                }
                else if (!GetComponent<GameEvent_Teleport>())
                {
                    gameObject.AddComponent<GameEvent_Teleport>();
                }
                break;
            default:
                break;
        }

        if (m_nextEvent)
        {
            if (!m_nextEvent.m_triggerType.Equals(GameEventTriggerType.OnSequence))
            {
                m_nextEvent.m_triggerType = GameEventTriggerType.OnSequence;
            }
        }
    }
}
