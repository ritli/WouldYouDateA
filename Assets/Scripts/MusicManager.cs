using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    AudioSource m_musicAudioSource;
    [Range(0, 1)]
    public float m_volume = 1;

    public AudioClip m_audioClip
    {
        get
        {
            if (m_musicAudioSource)
            {
                return m_musicAudioSource.clip;
            }

            return null;
        }
    }

	void Start () {
        m_musicAudioSource = GetComponent<AudioSource>();		
	}

    public IEnumerator FadeMusic(float time, float volume)
    {
        float timeElapsed = 0;

        while (timeElapsed < time)
        {
            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;

            m_volume = Mathf.Lerp(m_volume, volume, timeElapsed / time);

            SetVolume(m_volume, false);
        }
    }

    public void SetAudioClip(AudioClip clip)
    {
        m_musicAudioSource.clip = clip;
        m_musicAudioSource.Play();
    }

    void SetVolume(float volume, bool fade)
    {
        if (!fade)
        {
            m_musicAudioSource.volume = volume;

            return;
        }

        StartCoroutine(FadeMusic(0.2f, volume));
    }
}
