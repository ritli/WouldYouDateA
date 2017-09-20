
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
                    button.GetComponent<Button>().onClick.AddListener(() => GoodResponse(i));
                    break;
                case "NEUTRAL":
                    button.GetComponent<Button>().onClick.AddListener(() => NeutralResponse(i));
                    break;
                case "BAD":
                    button.GetComponent<Button>().onClick.AddListener(() => BadResponse(i));
                    break;

                default:
                    break;
            }

            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = s[1].TrimEnd();
        }
    }

    void GoodResponse(int index)
    {
        StartCoroutine(EndChoice());
        Manager.EndChoice(ChoiceType.good, currentCharacter, currentDialogue, index);   
    }
    void BadResponse(int index)
    {
        StartCoroutine(EndChoice());
        Manager.EndChoice(ChoiceType.bad, currentCharacter, currentDialogue, index);
    }
    void NeutralResponse(int index)
    {
        StartCoroutine(EndChoice());
        Manager.EndChoice(ChoiceType.neutral, currentCharacter, currentDialogue, index);
    }

    IEnumerator EndChoice()
    {
        TMPro.TextMeshProUGUI[] text = new TMPro.TextMeshProUGUI[Buttons.Count];

        int index = 0;

        foreach (GameObject g in Buttons)
        {
            g.GetComponent<Button>().interactable = false;
            g.GetComponent<Animator>().Play("Close");

            text[index] = g.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            index++;
        }

        float time = 0.3f;
        float timeElapsed = 0;

        while (timeElapsed < time)
        {
            foreach (TMPro.TextMeshProUGUI t in text)
            {
                t.color = new Color(t.color.r, t.color.g, t.color.b, 1 - timeElapsed / time + 0.00001f);
            }

            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;
        }

        yield return new WaitForSeconds(0.2f);

        foreach (GameObject g in Buttons)
        {
            Destroy(g);
        }
    }
}
