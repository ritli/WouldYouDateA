using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetspoController : MonoBehaviour {

     AudioClip m_normalTheme;
    public AudioClip m_battleTheme;

    Slider m_throwslider;
    public TMPro.TextMeshProUGUI m_GetFishText;
    public GameObject m_clickImage;
    public Image m_overlay;
    public bool m_receiveInput;
    bool m_Throwing;
    bool m_Fishing;
    float m_maxRot;

    public int m_reqClickCount = 20;

    int sliderMult = 1;
    int clickCount = 0;

    // Use this for initialization
    void Start () {
        m_normalTheme = Camera.main.GetComponent<AudioSource>().clip;

        m_throwslider = transform.parent.GetComponentInChildren<Slider>();

        m_clickImage.SetActive(false);
        m_GetFishText.gameObject.SetActive(false);

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
                float dir = -Input.GetAxis("Horizontal");

                transform.Rotate(Vector3.forward, dir * 10 * Time.deltaTime);

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
        Camera.main.GetComponent<AudioSource>().clip = m_battleTheme;
        Camera.main.GetComponent<AudioSource>().Play();

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
        m_GetFishText.gameObject.SetActive(true);


        m_overlay.color = new Color(m_overlay.color.r, m_overlay.color.g, m_overlay.color.b, 0);

        m_GetFishText.GetComponent<Animator>().Play("GetFish");
        m_GetFishText.text = "YOU CAUGHT A dead asp!";

        Camera.main.GetComponent<AudioSource>().clip = m_normalTheme;
        Camera.main.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(2f);

        m_GetFishText.gameObject.SetActive(false);

        m_Throwing = false;

        transform.rotation = Quaternion.identity;
        m_throwslider.value = 0;

        m_Fishing = false;
    }
}
