using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapHandler : MonoBehaviour {

    Animator m_animator;
    Image m_image;

    public void ChangeSceneTo(string name)
    {
        Manager.ChangeScene(name);
    }

	void Start () {
        m_animator = GetComponent<Animator>();
        m_image = GetComponent<Image>();

        m_animator.Play("CloseIdle");
    }

    void Update () {
		
	}

    public void Open()
    {
        m_image.enabled = true;

        m_animator.Play("Open");

    }

    public void Close()
    {
        m_animator.Play("Close");

      //  m_image.enabled = false;
    }


}
