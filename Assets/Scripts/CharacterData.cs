using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Characters
{
    Vegan, DJ, Master, Tiger, Hagbard, Olle, Asp, Fitkin
}

[System.Serializable]
public class CharacterData
{
    [SerializeField]
    Characters m_type;
    public Characters Type
    {
        get { return m_type; }
    }

    [SerializeField]
    private string m_name = "";
    public string Name
    {
        get{ return m_name; }
    }

    // Level of intimacy 0 <= x <= 5
    [SerializeField]
    private float m_intimacyLevel = 0f;
    public int IntimacyLevel
    {
        get{ return (int)m_intimacyLevel; }
    }

    [SerializeField]
    private GameObject m_charPrefab = null;
    public GameObject CharacterPrefab
    {
        get{ return m_charPrefab; }
    }

    [SerializeField]
    private AudioClip[] m_vocals;
    public AudioClip[] Vocals
    {
        get { return m_vocals; }
    }

    [SerializeField]
    private int m_talkRate = 4;
    public int TalkRate
    {
        get { return m_talkRate; }
    }
    /// <summary>
    /// Init character data.
    /// </summary>
    /// <param name="Intimacy">Clamped to 0f >= x >= 5f</param></param>
    /// <param name="prefab">Prefab.</param>
    public CharacterData(string name, float intimacy, GameObject prefab)
    {
        m_name = name;
        m_intimacyLevel = (intimacy < 0 ? 0 : intimacy) > 5 ? 5 : intimacy;
        m_charPrefab = prefab;
    }

    /// <summary>
    /// Empty constuctor.
    /// </summary>
    public CharacterData(){}

    public void InstantiateCharacter()
    {
        GameObject tmp = GameObject.Instantiate(m_charPrefab);
        tmp.SendMessage("LoadCharData", this);
    }

    /// <summary>
    /// Updates the intimacy.
    /// </summary>
    /// <param name="Intimacy">Clamped to 0 >= x >= 5</param>
    public void AddIntimacy(float intimacy)
    {
        m_intimacyLevel += intimacy;
       // m_intimacyLevel = (m_intimacyLevel < 0f ? 0f : m_intimacyLevel) > 5f ? 5f : m_intimacyLevel;
    }
}
