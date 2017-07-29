using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHolder : MonoBehaviour {

    public Sprite m_background;
    public string m_locationName;

	void Start () {
        InitializeScene();

        gameObject.SetActive(false);
	}
	
    void InitializeScene()
    {
        Manager.SetMapData(m_background, m_locationName);
    }
}
