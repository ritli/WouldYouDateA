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
    public GameObject m_splasheffect;
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
        StartCoroutine(DisplayText("PRESS THE LEFT MOUSE BUTTON TO FISH", 5f));
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

    void SpawnSplashEffect(Vector2 position)
    {
        print(position);

        Vector3 spawn = Vector3.zero;

        float scaleMult = 1 - spawn.y + 0.2f; 

        spawn.x = -(position.x - 0.5f) * 10f;
        spawn.y = (position.y - 1f) * 0.5f * 10;

        Vector3 scale = Vector3.one * scaleMult;

       

        GameObject g = Instantiate(m_splasheffect, spawn, m_splasheffect.transform.rotation, transform.parent);

        g.transform.localScale = scale;

        StartCoroutine(DestroyObject(g, 3f));
    }

    IEnumerator DestroyObject(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(Resources.Load("X", typeof(GameObject)), gameObject.transform.position, Quaternion.identity, transform.parent.GetChild(0));

        Destroy(gameObject);
    }

    IEnumerator Fish()
    {
        m_audio.PlayOneShot(m_throwingsound);

        yield return new WaitForSeconds(2.5f);

        Vector2 fishtarget;

        fishtarget.y = m_throwslider.value;
        fishtarget.x = (m_currentRot + m_maxRot) / (m_maxRot * 2);

        SpawnSplashEffect(fishtarget);

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

        FishItem item = GetFishReward(fishtarget);
        m_reqClickCount = item.m_reqClickCount;

        while (clickCount < m_reqClickCount)
        {
            m_overlay.color = new Color(m_overlay.color.r, m_overlay.color.g, m_overlay.color.b, (clickCount + 0.01f) / m_reqClickCount);

            yield return new WaitForEndOfFrame();
        }

        m_clickImage.SetActive(false);

        m_audio.Stop();

        m_overlay.color = new Color(m_overlay.color.r, m_overlay.color.g, m_overlay.color.b, 0);

        StartCoroutine(DisplayText("YOU CAUGHT A " + item.m_name));
        m_GetFishText.transform.Find("Sprite").GetComponentInChildren<Image>().sprite = item.m_sprite;

        yield return new WaitForSeconds(2.4f);

        m_audio.clip = m_normalTheme;
        m_audio.Play();

        yield return new WaitForSeconds(3f);

        m_Throwing = false;

        transform.rotation = Quaternion.identity;
        m_throwslider.value = 0;
        m_currentRot = 0;

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

    FishItem GetFishReward(Vector2 position)
    {
        GameObject f = (GameObject)Resources.Load("FishRewards", typeof(GameObject));

        print(f.name);

        int x = Mathf.FloorToInt((1 - position.x) * 10);
        int y = Mathf.FloorToInt((1 - position.y) * 10);

        //print(f.item.Length);

        return f.GetComponent<FishReward>().item[x + y * 10].GetComponent<FishItem>();
    }
}
