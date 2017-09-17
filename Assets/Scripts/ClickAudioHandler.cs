using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAudioHandler : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {

    public AudioClip[] m_clips = new AudioClip[2];
    AudioSource m_audio;
    bool m_audioPlayed = false;
    bool m_clickAudioPlayed = false;
    public float m_audioCooldown = 0.2f;

	void Start () {
        m_audio = gameObject.AddComponent<AudioSource>();
	}
	
    void EnableClickAudio()
    {
        m_clickAudioPlayed = false;
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

            Invoke("EnableAudio", m_audioCooldown);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!m_clickAudioPlayed)
        {
            m_clickAudioPlayed = true;

            m_audio.PlayOneShot(m_clips[1]);

            Invoke("EnableClickAudio", m_audioCooldown);
        }
    }
}
