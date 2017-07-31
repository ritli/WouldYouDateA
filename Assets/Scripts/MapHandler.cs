using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapHandler : MonoBehaviour {

    public AudioClip[] m_audioclips = new AudioClip[2];

    Animator m_animator;
    Image m_image;
    AudioSource m_audio;

    public void ChangeSceneTo(string name)
    {
        Manager.ChangeScene(name);
    }

	void Start () {
        m_animator = GetComponent<Animator>();
        m_image = GetComponent<Image>();
        m_audio = GetComponent<AudioSource>();

        m_animator.Play("CloseIdle");
    }

    public void Open()
    {
        m_audio.PlayOneShot(m_audioclips[0]);

        m_image.enabled = true;

        m_animator.Play("Open");
    }

    public void InstantClose() {
        if (m_animator)
        {
            m_animator.Play("CloseIdle");
        }
    }

    public void Close()
    {
        m_audio.PlayOneShot(m_audioclips[1]);

        m_animator.Play("Close");

      //  m_image.enabled = false;
    }


}
