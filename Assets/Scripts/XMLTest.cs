using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLTest : MonoBehaviour {

    public string m_path;

	// Use this for initialization
	void Start () {
        DialogueContainer c = DialogueContainer.Load(Characters.Hagbard);

        foreach(Dialogue d in c.m_dialogues)
        {
            print(d.text); 
        }

      // testReader test = new testReader();
        //test.readXML();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}   
