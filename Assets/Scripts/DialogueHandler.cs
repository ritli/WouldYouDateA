using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour {

    Text m_text;

    private string m_dialogueToPrint;
    string m_senderName;

    public int m_maxTextCount;

    public AudioClip[] m_chatAudioClips;
    public AudioClip m_chatOpenClip;
    public AudioClip m_chatCloseClip;

    AudioSource m_audio;
    Animator m_animator;
    string m_nameText;

    const float m_printInterval = 0.02f;

    void Start()
    {
        m_text = GetComponentInChildren<Text>();

        m_animator = GetComponent<Animator>();
        m_audio = GetComponent<AudioSource>();
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

    public void PrintText(string name, string text)
    {
       // m_image.sprite = portrait;
       // m_image.color = color;
        m_dialogueToPrint = text;
        m_senderName = name;
        StartCoroutine(PrintLoop());
    }

    string GetName()
    {
        return m_senderName + ": ";
    }

    IEnumerator PrintLoop()
    {
        //m_nameText.transform.parent.gameObject.SetActive(true);
        m_text.text = GetName();

        PlayOpenAnim();
        PlayOpenSound();
            
        yield return new WaitForSeconds(0.5f);

        int wordCount = 0;

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

                m_text.text = GetName();
            }

            for (int c = 0; c < words[i].Length; c++)
            {
                m_text.text += words[i][c];
                yield return new WaitForSeconds(m_printInterval);

                if (wordCount % 4 == 0)
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


        PlayCloseAnim();
        PlayCloseSound();

       // m_nameText.transform.parent.gameObject.SetActive(false);
    }

    void PlayOpenAnim()
    {
        m_animator.Play("Up");
    }

    void PlayCloseAnim()
    {
        m_animator.Play("Down");
    }
}


