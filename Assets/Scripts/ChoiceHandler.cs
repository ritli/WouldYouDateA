using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    

public class ChoiceHandler : MonoBehaviour {

    public Button m_buttonTemplate;
    public ChoiceTreeTemplate template;

	// Use this for initialization
	void Start () {
        StartChoiceEvent(template);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartChoiceEvent(ChoiceTreeTemplate questions)
    {
        // Clear old choices
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0));
        }

        // Select possible question
        List<ChoiceNode> viable = new List<ChoiceNode>();

        foreach (ChoiceNode question in questions.choices)
        {
            if (!question.passed)
            {
                
            }
        }
        /*
        for (int i = 0; i < questions.buttonNames.Length; i++)
        {
            Instantiate(m_buttonTemplate, transform).GetComponentInChildren<Text>().text = choices.buttonNames[i];
        }
        */
    }
}
