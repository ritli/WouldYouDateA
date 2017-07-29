using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    

public class ChoiceHandler : MonoBehaviour {

    [SerializeField] private Button m_buttonTemplate;

    void StartChoiceEvent(ChoiceTreeTemplate questions, int intimacy)
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
            if (!question.passed && intimacy >= question.reqIntimacy)
            {
                if (question.prereqChoice == null)
                    viable.Add(question);
                else
                {
                    foreach (ChoiceNode comparison in questions.choices)
                    {
                        if (question.prereqChoice == comparison && comparison.passed == true)
                            viable.Add(question);
                    }
                }
            }
        }

        ChoiceNode chosenQuestion = viable[Random.Range(0, viable.Count - 1)];


        for (int i = 0; i < chosenQuestion.buttonNames.Length; i++)
        {
            Instantiate(m_buttonTemplate, transform).GetComponentInChildren<Text>().text = chosenQuestion.buttonNames[i];
        }

    }
}
