using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHolder : MonoBehaviour {

    public Sprite m_background;

	void Start () {
        InitializeScene();

        gameObject.SetActive(false);
	}
	
    void InitializeScene()
    {
        Manager.SetMapData(m_background);
    }
}
