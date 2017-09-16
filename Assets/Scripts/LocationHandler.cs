using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationHandler : MonoBehaviour {

    TMPro.TextMeshProUGUI m_text;
    	
	void Start()
    {
        m_text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    public void SetLocationText(string location)
    {
        m_text.text = location;
    }
}
