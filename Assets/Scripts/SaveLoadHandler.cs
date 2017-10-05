using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadHandler : MonoBehaviour {

    Animator m_animator;
    bool m_open = false;

	void Start () {
        m_animator = GetComponent<Animator>();    		
	}
	
    public void Init()
    {

    }

	void Update () {
		
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
