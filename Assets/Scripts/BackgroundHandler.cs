using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundHandler : MonoBehaviour {

    Image m_image;

    void Start()
    {
        m_image = GetComponent<Image>();
    }

    public void SetBackground(Sprite image)
    {
        m_image.sprite = image;
    }
}
