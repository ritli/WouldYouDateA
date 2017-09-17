using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepButton : MonoBehaviour {

	void Start () {
        GetComponent<Button>().onClick.AddListener(Sleep);       		
	}
	
    void Sleep()
    {
        Manager.EndDay();

        GetComponent<Button>().interactable = false;

        Invoke("EnableButton", 0.7f);
    }

    void EnableButton()
    {
        GetComponent<Button>().interactable = true;
    }
}
