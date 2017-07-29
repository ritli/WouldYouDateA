﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    Manager m_instance;

    static private List<CharacterData> m_charData();

	void Start () {
        if (FindObjectsOfType<Manager>().Length != 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            m_instance = this;
        }
    
    }
	
    static void ChangeScene(string name)
    {

    }

	void Update () {
    		
	}
}
