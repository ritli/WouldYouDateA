using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StopFishButton : MonoBehaviour {

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoFish);
    }

    void GoFish()   
    {
        Manager.SetGameState(GameState.explore);
        Manager.ChangeScene("Lake");

        GetComponent<Button>().interactable = false;

        Invoke("EnableButton", 0.7f);
    }

    void EnableButton()
    {
        GetComponent<Button>().interactable = true;
    }
}
