using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        

        testReader test = new testReader();
        test.readXML();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
