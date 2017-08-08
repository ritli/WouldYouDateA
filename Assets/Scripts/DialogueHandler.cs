using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour {

    Text m_text;
    Animator m_nameAnimator;
    Text m_nameText;

    [Multiline]
    public string m_dialogueToPrint;
    string m_senderName;

    public int m_maxTextCount;

    public AudioClip[] m_chatAudioClips;
    public AudioClip m_chatOpenClip;
    public AudioClip m_chatCloseClip;

    AudioSource m_audio;
    Animator m_animator;
    int m_talkRate = 4;

    const float m_printInterval = 0.03f;

    void Start()
    {
        m_text = GetComponentInChildren<Text>();

        GameObject namepanel = transform.parent.Find("NamePanel").gameObject;

        m_nameAnimator = namepanel.GetComponent<Animator>();
        m_nameText = namepanel.GetComponentInChildren<Text>();
        m_animator = GetComponent<Animator>();
        m_audio = GetComponent<AudioSource>();

       // PrintText("The DJ", m_dialogueToPrint);
    }

    private void OnEnable()
    {
        m_nameAnimator.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        m_nameAnimator.gameObject.SetActive(false);
    }

    void PlayRandomAudio()
    {
        m_audio.PlayOneShot(m_chatAudioClips[Random.Range(0, m_chatAudioClips.Length)]);
    }

    void PlayOpenSound()
    {
        m_audio.PlayOneShot(m_chatOpenClip);
    }

    void PlayCloseSound()
    {
        m_audio.PlayOneShot(m_chatCloseClip);
    }

    public void PrintText(Dialogue dialogue, CharacterData characterData, bool hasChoices)
    {
        m_chatAudioClips = characterData.Vocals;
        m_talkRate = characterData.TalkRate;
        m_dialogueToPrint = dialogue.text.Trim();
        m_senderName = characterData.Name.ToString();
        StartCoroutine(PrintLoop(dialogue, hasChoices, characterData));
    }

    string GetName()
    {
        return m_senderName;
    }

    IEnumerator PrintLoop(Dialogue dialogue, bool hasChoices, CharacterData characterData)
    {
        //m_nameText.transform.parent.gameObject.SetActive(true);
        m_nameText.text = GetName();
        m_text.text = "";

        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("IdleUp"))
        {
            PlayOpenAnim();
            PlayOpenSound();
        }
            
        yield return new WaitForSeconds(0.5f);

        int wordCount = 0;

        m_dialogueToPrint = m_dialogueToPrint.Trim();

        string[] words = m_dialogueToPrint.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (i != 0)
            {
                m_text.text += " ";
                yield return new WaitForSeconds(m_printInterval);
            }

            if (wordCount + words[i].Length > m_maxTextCount)
            {
                while (!Input.GetButton("Fire1"))
                {
                    yield return new WaitForEndOfFrame();
                }

                wordCount = 0;

                m_text.text = "";
            }

            for (int c = 0; c < words[i].Length; c++)
            {
                m_text.text += words[i][c];
                yield return new WaitForSeconds(m_printInterval);

                if (wordCount % m_talkRate == 0)
                {
                    PlayRandomAudio();
                }

                if (m_dialogueToPrint[i] == '\n')
                {
                    yield return new WaitForSeconds(m_printInterval * 10);
                }

                wordCount++;
            }
        }

        while (!Input.GetButton("Fire1"))
        {
            yield return new WaitForEndOfFrame();
        }

        Manager.EndDialogue(dialogue, hasChoices, characterData);

       // m_nameText.transform.parent.gameObject.SetActive(false);
    }

    public void Close()
    {
        PlayCloseAnim();
        PlayCloseSound();
    }

    void PlayOpenAnim()
    {
        m_nameAnimator.Play("Up");
        m_animator.Play("Up");
    }

    void PlayCloseAnim()
    {
        m_nameAnimator.Play("Down");
        m_animator.Play("Down");
    }
}


