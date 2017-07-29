using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    

public class ChoiceHandler : MonoBehaviour {

    public Button m_buttonTemplate;
    public ChoiceTemplate template;

	// Use this for initialization
	void Start () {
       // StartChoiceEvent(template);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartChoiceEvent(ChoiceTemplate choices)
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0));
        }

        for (int i = 0; i < choices.buttonNames.Length; i++)
        {
            Instantiate(m_buttonTemplate, transform).GetComponentInChildren<Text>().text = choices.buttonNames[i];
        }
    }
}
