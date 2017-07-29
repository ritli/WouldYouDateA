using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour {

    [SerializeField] private string m_charName;
    [SerializeField][Range(0.1f, 100f)] private float m_encounterChance; 

	// Use this for initialization
	void Start () 
    {
        //GameObject.FindGameObjectWithTag("Manager")().GetComponent<Manager>()
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
