using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAudioHandler : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {

    public AudioClip[] m_clips = new AudioClip[2];
    AudioSource m_audio;

	void Start () {
        m_audio = gameObject.AddComponent<AudioSource>();
	}
	
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_audio.PlayOneShot(m_clips[0]);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_audio.PlayOneShot(m_clips[1]);
    }
}
