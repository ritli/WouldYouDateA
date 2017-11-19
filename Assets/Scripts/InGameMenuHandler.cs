using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InGameMenuHandler : MonoBehaviour {

    bool m_open = false;
    public Animator m_savedgameAnim;

	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().Play("CloseIdle");
        }
    }

    public void ExitToMenu()
    {
        ToggleMenu();
        Manager.ChangeScene("Menu");
    }

    public void SaveGame()
    {
        ProgressManager.current.currentScene = SceneManager.GetActiveScene().name;
        ProgressManager.current.date = Manager.GetTime() + " " + Manager.GetDay().ToString();

        SaveLoad.Load();
        SaveLoad.Save();

        m_savedgameAnim.Play("Save");
    }
	
   

    public void ToggleMenu()
    {
        string StateName = "Open";

        if (m_open)
        {
            StateName = "Close";
        }

        m_open = !m_open;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().Play(StateName);
        }
    }
}
