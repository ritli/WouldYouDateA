using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetspoController : MonoBehaviour {

    AudioClip m_normalTheme;

    [Header("Audio")]
    public AudioClip m_battleTheme;
    public AudioClip m_throwingsound;
    public AudioClip m_winningsound;

    [SerializeField]
    public FishItem[,] m_fishItems = new FishItem[10,10]; 

    AudioSource m_audio;

    Slider m_throwslider;

    [Header("References")]

    public TMPro.TextMeshProUGUI m_GetFishText;
    public GameObject m_clickImage;
    public Image m_overlay;
    public bool m_receiveInput;
    bool m_Throwing;
    bool m_Fishing;
    float m_maxRot = 60;
    float m_currentRot = 0;

    public int m_reqClickCount = 20;

    int sliderMult = 1;
    int clickCount = 0;

    void Start () {
        m_audio = GetComponent<AudioSource>();

        m_normalTheme = m_audio.clip;

        m_throwslider = transform.parent.GetComponentInChildren<Slider>();

        m_clickImage.SetActive(false);
        m_GetFishText.gameObject.SetActive(false);

        StartCoroutine(DisplayText("PRESS A OR D TO MOVE THE METSPO", 1f));
        StartCoroutine(DisplayText("PRESS THE LEFT MOUSE BUTTON TO FISH", 4f));
    }

    // Update is called once per frame
    void Update () {
		if (m_receiveInput)
        {
            if (m_Throwing && !m_Fishing)
            {
                if (m_throwslider.value == 1 || m_throwslider.value == 0)
                {
                    m_throwslider.value = Mathf.Clamp01(m_throwslider.value);

                    sliderMult = -sliderMult;
                }

                m_throwslider.value += sliderMult * Time.deltaTime;

                transform.Rotate(Vector3.left, m_throwslider.value * -sliderMult * 100 * Time.deltaTime);

                if (Input.GetButtonDown("Fire1"))
                {
                    m_Fishing = true;

                    StartCoroutine(Fish());
                }
            }
            else
            {
                float dir = -Input.GetAxis("Horizontal") * 20 * Time.deltaTime;

                m_currentRot += dir;

                if (!(m_currentRot <= m_maxRot && m_currentRot >= -m_maxRot))
                {
                    dir = 0;

                    m_currentRot = Mathf.Clamp(m_currentRot, -m_maxRot, m_maxRot);
                }
                
                transform.Rotate(Vector3.forward, dir);

                if (Input.GetButtonDown("Fire1"))
                {
                    m_Throwing = true;
                }
            }
        }

        if (m_Fishing)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                clickCount++;
            }
        }
    }

    IEnumerator Fish()
    {
        m_audio.PlayOneShot(m_throwingsound);

        yield return new WaitForSeconds(2.5f);

        m_audio.clip = m_battleTheme;
        m_audio.Play();

        float time = 0;

        while (time < 2.4f)
        {
            m_overlay.color = new Color(m_overlay.color.r, m_overlay.color.g, m_overlay.color.b, time % 0.5f);

            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }

        clickCount = 0;

        m_clickImage.SetActive(true);

        while (clickCount < m_reqClickCount)
        {
            m_overlay.color = new Color(m_overlay.color.r, m_overlay.color.g, m_overlay.color.b, (clickCount + 0.01f) / m_reqClickCount);

            yield return new WaitForEndOfFrame();

        }

        m_clickImage.SetActive(false);


        m_overlay.color = new Color(m_overlay.color.r, m_overlay.color.g, m_overlay.color.b, 0);

        m_audio.Stop();

        StartCoroutine(DisplayText("YOU CAUGHT A FISH"));

        yield return new WaitForSeconds(1);

        m_audio.clip = m_normalTheme;
        m_audio.Play();

        yield return new WaitForSeconds(2f);

        m_Throwing = false;

        transform.rotation = Quaternion.identity;
        m_throwslider.value = 0;

        m_Fishing = false;
    }

    IEnumerator DisplayText(string text)
    {
        m_GetFishText.gameObject.SetActive(true);

        m_GetFishText.GetComponent<Animator>().Play("GetFish");
        m_GetFishText.text = text;
        m_audio.PlayOneShot(m_winningsound);

        yield return new WaitForSeconds(2.4f);

        m_GetFishText.gameObject.SetActive(false);

    }

    IEnumerator DisplayText(string text, float delay)
    {
        yield return new WaitForSeconds(delay);

        StartCoroutine(DisplayText(text));
    }
}
