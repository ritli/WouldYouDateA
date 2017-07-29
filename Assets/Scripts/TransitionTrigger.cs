using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransitionTrigger : MonoBehaviour{

    Button m_button;
    string sceneToChangeTo;

    void Start () {
        m_button = GetComponent<Button>();

        m_button.onClick.AddListener(ChangeScene);
	}
	
    public void SetText(string name)
    {
        sceneToChangeTo = name;
    }

    void ChangeScene()
    {
        Manager.ChangeScene(sceneToChangeTo);
    }

}
