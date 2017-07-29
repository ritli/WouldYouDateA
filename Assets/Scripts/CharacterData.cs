using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
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
        m_intimacyLevel = (m_intimacyLevel < 0f ? 0f : m_intimacyLevel) > 5f ? 5f : m_intimacyLevel;
    }
}
