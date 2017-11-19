using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {

    Animator m_animator;
    bool m_open = false;

    void Start () {
        m_animator = GetComponent<Animator>();
	}
	
    public void ShowLoadMenu()
    {
        if (m_open)
        {
            m_open = false;
            m_animator.Play("Close");
        }
        else
        {
            m_open = true;
            m_animator.Play("Open");
        }
    }
}
