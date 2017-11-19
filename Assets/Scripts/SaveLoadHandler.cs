using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadHandler : MonoBehaviour {

    Animator m_animator;
    bool m_open = false;

	void Start () {
        m_animator = GetComponent<Animator>();

        Button[] buttons = GetComponentsInChildren<Button>();

        buttons[0].onClick.AddListener(LoadSave1);
        buttons[1].onClick.AddListener(LoadSave2);
        buttons[2].onClick.AddListener(LoadSave3);

        buttons = transform.GetChild(3).GetComponentsInChildren<Button>();

        buttons[0].onClick.AddListener(DeleteSave1);
        buttons[1].onClick.AddListener(DeleteSave2);
        buttons[2].onClick.AddListener(DeleteSave3);

        Init();
    }

    public void Init()
    {
        SaveLoad.Load();

        int index = 0;

        for (int i = index; i < 3; i++)
        {
            transform.GetChild(i).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Empty";
        }

        foreach(ProgressManager p in SaveLoad.savedGames)
        {
            if (transform.GetChild(p.index) && p.index < 3){
                transform.GetChild(p.index).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Day " + p.daysPassed + " at " + p.currentSceneName;
            }
        }
    }

        void LoadSave1()
    {
        LoadSaveIndex(0);
    }

    void LoadSave2()
    {
        LoadSaveIndex(1);
    }

    void LoadSave3()
    {
        LoadSaveIndex(2);
    }

    void DeleteSave1()
    {
        DeleteSave(0);
    }
    void DeleteSave2()
    {
        DeleteSave(1);

    }
    void DeleteSave3()
    {
        DeleteSave(2);
    }

    void LoadSaveIndex(int index)
    {
        if (m_open)
        {
            ShowLoadMenu();
        }

        SaveLoad.Load();

        if (transform.GetChild(index).GetComponentInChildren<TMPro.TextMeshProUGUI>().text != "Empty")
        {
            foreach (ProgressManager p in SaveLoad.savedGames)
            {
                if (p.index == index)
                {
                    ProgressManager.current = p;
                }
            }
        }
        else {
            ProgressManager.current = new ProgressManager(System.DateTime.Today.ToString(), "VillaGrutIntro", index);
        }

        Manager.PlayFromMenuLoad();
    }

    void DeleteSave(int index)
    {
        SaveLoad.Load();

        for (int i = 0; i < SaveLoad.savedGames.Count; i++)
        {
            if (SaveLoad.savedGames[i].index == index)
            {
                SaveLoad.savedGames.RemoveAt(i);
            }
        }

        transform.GetChild(index).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Empty";

        SaveLoad.Save();
    }

    public void StartNewGame()
    {
        for (int i = 0; i < 3; i++)
        {
            if (transform.GetChild(i).GetComponentInChildren<TMPro.TextMeshProUGUI>().text == "Empty")
            {
                LoadSaveIndex(i);
                break;
            }
        }

        DeleteSave(0);
        LoadSaveIndex(0);
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
