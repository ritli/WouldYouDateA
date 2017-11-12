using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FishButton : MonoBehaviour {

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoFish);
    }

    void GoFish()   
    {
        Manager.SetGameState(GameState.fish);
        Manager.ChangeScene("Fish");

       // Manager.m_instance.transform.GetChild(0).gameObject.SetActive(true);

        GetComponent<Button>().interactable = false;

        Invoke("EnableButton", 0.7f);
    }

    void EnableButton()
    {
        GetComponent<Button>().interactable = true;
    }
}
