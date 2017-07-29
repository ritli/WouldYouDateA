using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private CharacterData characterData;

	// Use this for initialization
	void Start () 
    {
        characterData = new CharacterData();
	}

    /// <summary>
    /// Data is passed by the instantiating function.
    /// </summary>
    /// <param name="data">Data.</param>
    void LoadCharData(ref CharacterData data)
    {
        characterData = data;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
