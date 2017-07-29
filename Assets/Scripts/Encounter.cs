using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
internal struct EncounterChance
{
    public string m_charName;
    [Range(0.1f, 100f)]
    public float m_encounterChance;
}

public class Encounter : MonoBehaviour {

    [SerializeField] private EncounterChance[] m_possibleEncounters;

	// Use this for initialization
	void Start () 
    {
        List<string> encountered = new List<string>();
        foreach (EncounterChance possible in m_possibleEncounters)
        {
            if (Random.Range(0f, 100f) <= possible.m_encounterChance)
                encountered.Add(possible.m_charName);
        }
        //GameObject.FindGameObjectWithTag("Manager")().GetComponent<Manager>()
	}
}
