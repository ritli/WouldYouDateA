using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroHandler : MonoBehaviour {

    public void PlayGame()
    {
        GetComponent<Animator>().Play("Exit");

        Invoke("DelayedPlay", 1.2f);
    }

    void DelayedPlay() {
        SceneManager.LoadScene("Menu");
    }
}
