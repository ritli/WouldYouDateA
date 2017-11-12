using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    Animator m_animator;

    public string m_stateName;

	void Awake () {
        m_animator = GetComponent<Animator>();

        m_animator.Play(m_stateName);

        print("PLAYING");
    }

    void Update()
    {
        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            m_animator.Play(m_stateName, 0);


            m_animator.Play(m_stateName);
            enabled = false;
        }
    }
}
