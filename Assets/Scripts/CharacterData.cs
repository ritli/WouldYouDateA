using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
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
    /// <param name="Intimacy">Clamped to 0 <c>= x <c>= 5</param></param>
    /// <param name="prefab">Prefab.</param>
    public CharacterData(int Intimacy, GameObject prefab)
    {
        m_intimacyLevel = (Intimacy < 0 ? 0 : Intimacy) > 5 ? 5 : Intimacy;
        m_charPrefab = prefab;
    }

    public void InstantiateCharacter()
    {
        GameObject tmp = GameObject.Instantiate(m_charPrefab);
        tmp.SendMessage("LoadCharData", this);

    }

    /// <summary>
    /// Updates the intimacy.
    /// </summary>
    /// <param name="Intimacy">Clamped to 0 <c>= x <c>= 5</param>
    public void UpdateIntimacy(int Intimacy)
    {
        m_intimacyLevel = (Intimacy < 0 ? 0 : Intimacy) > 5 ? 5 : Intimacy;
    }
}
