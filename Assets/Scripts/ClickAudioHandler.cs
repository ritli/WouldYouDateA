using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAudioHandler : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {

    public AudioClip[] m_clips = new AudioClip[2];
    AudioSource m_audio;
    bool m_audioPlayed = false;

	void Start () {
        m_audio = gameObject.AddComponent<AudioSource>();
	}
	
    void EnableAudio()
    {
        m_audioPlayed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!m_audioPlayed)
        {
            m_audioPlayed = true;

            m_audio.PlayOneShot(m_clips[0]);

            Invoke("EnableAudio", 0.2f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_audio.PlayOneShot(m_clips[1]);
    }
}
