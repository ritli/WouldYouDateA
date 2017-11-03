using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour {

    TMPro.TextMeshProUGUI m_text;
    Animator m_nameAnimator;
    TMPro.TextMeshProUGUI m_nameText;
    DotDotDotAnimator m_dots;


    [Multiline]
    public string m_dialogueToPrint;
    string m_senderName;

    public int m_maxLetterCount = 140;
    int m_standardLetterCount;
    int m_tigerLetterCount;

    int m_standardFontSize;
    int m_tigerFontSize;

    public AudioClip[] m_chatAudioClips;
    public AudioClip m_chatOpenClip;
    public AudioClip m_chatCloseClip;

    AudioSource m_audio;
    Animator m_animator;
    int m_talkRate = 4;

    const float m_printInterval = 0.03f;

    bool m_InDialogue = false; //Whether a dialogue is active
    bool m_DialogueFinished = false; //
    bool m_DialogueWaiting = false;
    bool m_SkipDialogue = false; //When true, coroutine will skip to end of current dialogue on next letter

    float m_timeInDialogue = 0;

    void Start()
    {
        m_standardLetterCount = m_maxLetterCount;
        m_tigerLetterCount = m_maxLetterCount / 2;

        m_text = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        m_standardFontSize = (int)m_text.fontSize;
        m_tigerFontSize = m_standardFontSize - 15;

        GameObject namepanel = transform.parent.Find("NamePanel").gameObject;

        m_nameAnimator = namepanel.GetComponent<Animator>();
        m_nameText = namepanel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        m_animator = GetComponent<Animator>();
        m_audio = GetComponent<AudioSource>();
        m_dots = GetComponentInChildren<DotDotDotAnimator>();
    }

    private void Update()
    {
        if (m_InDialogue && !m_DialogueFinished && m_timeInDialogue > 0.3f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                m_SkipDialogue = true;
            }
        }
        else if (m_InDialogue && m_DialogueFinished)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                m_DialogueWaiting = true;
            }
        }

        if (m_InDialogue)
        {
            m_timeInDialogue += Time.deltaTime;
        }
        else
        {
            m_timeInDialogue = 0;
        }

        if (m_DialogueFinished)
        {
            m_dots.Show();
        }
        else
        {
            m_dots.Hide();
        }
    }

    private void OnEnable()
    {
        //m_nameAnimator.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
       // m_nameAnimator.gameObject.SetActive(false);
    }

    void PlayRandomAudio()
    {
        if (m_chatAudioClips.Length > 0)
        {
            m_audio.PlayOneShot(m_chatAudioClips[Random.Range(0, m_chatAudioClips.Length)]);
        }
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

        StartCoroutine(PrintLoopStandard(dialogue, hasChoices, characterData));
    }

    public void PrintResponse(string text, CharacterData characterData, Dialogue dialogue, bool badChoice)
    {
        m_chatAudioClips = characterData.Vocals;
        m_talkRate = characterData.TalkRate;
        m_dialogueToPrint = text.Trim();
        m_senderName = characterData.Name.ToString();

        StartCoroutine(PrintLoopResponse(text, characterData, dialogue, badChoice));
    }

    public void PrintTextEvent(string text, string name, GameEvent nextEvent)
    {
        m_chatAudioClips = new AudioClip[0];
        m_talkRate = 6;
        m_dialogueToPrint = text.Trim();
        m_senderName = name;

        StartCoroutine(PrintLoopEvent(text, nextEvent));
    }

    string GetName()
    {
        return m_senderName;
    }

    bool Test()
    {
        return true;
    }

    IEnumerator PrintLoopEvent(string text, GameEvent nextEvent)
    {
        m_maxLetterCount = m_standardLetterCount;
        m_text.fontSize = m_standardFontSize;
        m_text.font = Resources.Load<TMPro.TMP_FontAsset>("DialogueFont");

        StartCoroutine(Print(text));

        while (m_InDialogue)
        {
            yield return new WaitForEndOfFrame();
        }

        Manager.EndEvent(nextEvent);
    }

    IEnumerator PrintLoopResponse(string text, CharacterData characterData, Dialogue dialogue, bool badChoice)
    {
        if (characterData.Type == Characters.Tiger)
        {
            m_text.fontSize = m_tigerFontSize;
            m_maxLetterCount = m_tigerLetterCount;
            m_text.font = Resources.Load<TMPro.TMP_FontAsset>("TigerFont");
        }
        else
        {
            m_maxLetterCount = m_standardLetterCount;
            m_text.fontSize = m_standardFontSize;
            m_text.font = Resources.Load<TMPro.TMP_FontAsset>("DialogueFont");
        }

        StartCoroutine(Print(text));

        while (m_InDialogue)
        {
            yield return new WaitForEndOfFrame();
        }


        Manager.EndResponse(dialogue, characterData, badChoice);
    }

    IEnumerator PrintLoopStandard(Dialogue dialogue, bool hasChoices, CharacterData characterData)
    {
        if (characterData.Type == Characters.Tiger)
        {
            m_text.fontSize = m_tigerFontSize;
            m_maxLetterCount = m_tigerLetterCount;
            m_text.font = Resources.Load<TMPro.TMP_FontAsset>("TigerFont");
        }
        else
        {
            m_maxLetterCount = m_standardLetterCount;
            m_text.fontSize = m_standardFontSize;
            m_text.font = Resources.Load<TMPro.TMP_FontAsset>("DialogueFont");
        }

        StartCoroutine(Print(dialogue.text));

        while (m_InDialogue)
        {
            yield return new WaitForEndOfFrame();
        }

        Manager.EndDialogue(dialogue, hasChoices, characterData);
    }

    IEnumerator Print(string text)
    {
        m_SkipDialogue = false;
        m_InDialogue = true;

        m_nameText.text = GetName();
        m_text.text = "";

        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("IdleUp"))
        {
            PlayOpenAnim();
            PlayOpenSound();
        }

        yield return new WaitForSeconds(0.5f);

        int letterCount = 0;

        m_dialogueToPrint = m_dialogueToPrint.Trim();

        string[] words = m_dialogueToPrint.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (i != 0)
            {
                m_text.text += " ";
                yield return new WaitForSeconds(m_printInterval);
            }

            if (letterCount + words[i].Length > m_maxLetterCount)
            {
                m_DialogueFinished = true;
                m_DialogueWaiting = false;

                while (!m_DialogueWaiting)
                {
                    yield return new WaitForEndOfFrame();
                }

                m_SkipDialogue = false;
                m_DialogueFinished = false;

                letterCount = 0;

                m_text.text = "";
            }

            for (int c = 0; c < words[i].Length; c++)
            {
                m_text.text += words[i][c];
                yield return new WaitForSeconds(m_printInterval);

                if (letterCount % m_talkRate == 0)
                {
                    PlayRandomAudio();
                }

                if (m_dialogueToPrint[i] == '\n')
                {
                    yield return new WaitForSeconds(m_printInterval * 10);
                }

                letterCount++;

                if (m_SkipDialogue)
                {
                    int lastIndex = c + 1;
                    int continueIndex = 0;

                    while (letterCount + words[i].Length < m_maxLetterCount)
                    {
                        for (int d = lastIndex; d < words[i].Length; d++)
                        {
                            m_text.text += words[i][d];
                            letterCount++;
                            continueIndex = d;
                        }

                        m_text.text += " ";

                        if (i == words.Length - 1)
                        {
                            break;
                        }
                        i++;

                        lastIndex = 0;
                    }

                    c = 1000;

                    m_DialogueFinished = true;
                    m_DialogueWaiting = true;

                    while (!m_DialogueWaiting)
                    {
                        yield return new WaitForEndOfFrame();
                    }

                    m_DialogueWaiting = true;
                    m_DialogueFinished = false;
                    m_SkipDialogue = false;
                }
            }
        }

        m_DialogueFinished = true;
        m_DialogueWaiting = false;

        while (!m_DialogueWaiting)
        {
            yield return new WaitForEndOfFrame();
        }

        m_SkipDialogue = false;
        m_DialogueFinished = false;
        m_InDialogue = false;

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


