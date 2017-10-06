using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour {

	void Start () {
        Init();
	}
	
    public void Init()
    {
        transform.Find("Play").GetComponent<Animator>().Play("Open");
        transform.Find("Options").GetComponent<Animator>().Play("Open");
        transform.Find("Load").GetComponent<Animator>().Play("Open");
        transform.Find("Exit").GetComponent<Animator>().Play("Open");
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Quit()
    {
        Manager.Quit();
    }

    public void StartGame()
    {
        Manager.PlayFromMenu();
    }

    public void OpenLoadMenu()
    {

    }
}
