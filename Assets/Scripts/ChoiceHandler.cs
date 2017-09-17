
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ChoiceType
{
    good, neutral, bad
}    

public class ChoiceHandler : MonoBehaviour {

    [SerializeField] private GameObject m_buttonTemplate;
    CharacterData currentCharacter;
    Dialogue currentDialogue;

    List<GameObject> Buttons;

    public void StartChoiceEvent(Dialogue dialogue, CharacterData characterData)
    {
        currentCharacter = characterData;
        currentDialogue = dialogue;
        Buttons = new List<GameObject>(dialogue.choices.Count);

        for (int i = 0; i < dialogue.choices.Count; i++)
        {
            GameObject button = Instantiate(m_buttonTemplate, transform);

            Buttons.Add(button);

            string[] s = dialogue.choices[i].Split('+');

            switch (s[0].Trim())
            {
                case "GOOD":
                    button.GetComponent<Button>().onClick.AddListener(() => GoodResponse());
                    break;
                case "NEUTRAL":
                    button.GetComponent<Button>().onClick.AddListener(() => NeutralResponse());
                    break;
                case "BAD":
                    button.GetComponent<Button>().onClick.AddListener(() => BadResponse());
                    break;

                default:
                    break;
            }

            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = s[1];
        }
    }

    void GoodResponse()
    {
        StartCoroutine(EndChoice());
        Manager.EndChoice(ChoiceType.good, currentCharacter, currentDialogue);
    }
    void BadResponse()
    {
        StartCoroutine(EndChoice());
        Manager.EndChoice(ChoiceType.bad, currentCharacter, currentDialogue);
    }
    void NeutralResponse()
    {
        StartCoroutine(EndChoice());
        Manager.EndChoice(ChoiceType.neutral, currentCharacter, currentDialogue);
    }

    IEnumerator EndChoice()
    {
        foreach(GameObject g in Buttons)
        {
            g.GetComponent<Button>().interactable = false;
            g.GetComponent<Animator>().Play("Close");
        }

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject g in Buttons)
        {
            Destroy(g);
        }
    }
}
