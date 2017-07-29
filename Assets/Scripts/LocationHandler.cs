using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationHandler : MonoBehaviour {

    Text m_text;
    	
	void Start()
    {
        m_text = GetComponentInChildren<Text>();
    }

    public void SetLocationText(string location)
    {
        m_text.text = location;
    }
}
