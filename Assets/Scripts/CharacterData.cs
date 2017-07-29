using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    private string m_name = "";
    public string Name
    {
        get{ return m_name; }
    }

    // Level of intimacy 0 <= x <= 5
    private int m_intimacyLevel = 0;
    public int IntimacyLevel
    {
        get{ return m_intimacyLevel; }
    }

    private GameObject m_charPrefab = null;
    public GameObject CharacterPrefab
    {
        get{ return m_charPrefab; }
    }

    /// <summary>
    /// Init character data.
    /// </summary>
    /// <param name="Intimacy">Clamped to 0 >= x >= 5</param></param>
    /// <param name="prefab">Prefab.</param>
    public CharacterData(string name, int intimacy, GameObject prefab)
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
    public void UpdateIntimacy(int intimacy)
    {
        m_intimacyLevel = (intimacy < 0 ? 0 : intimacy) > 5 ? 5 : intimacy;
    }
}
