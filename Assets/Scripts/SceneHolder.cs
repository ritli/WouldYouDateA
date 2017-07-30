using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MapData
{
    public GameObject[] characters;
    public Sprite background;
    public string locationName;
    [Header("Order: Up, Down, Left, Right")]
    public string[] arrowLocations;
}

public class SceneHolder : MonoBehaviour {

    public MapData mapData;

	void Start () {
        if (FindObjectOfType<Manager>())
        {
            Invoke("InitializeScene", 0.001f);

        }
        else
        {
            Invoke("InitializeScene", 0.05f);
        }        

	}
	
    void InitializeScene()
    {
        if (mapData.arrowLocations.Length != 4)
        {
            string[] sArray = new string[4];

            for (int i = 0; i < 4; i++)
            {
                sArray[i] = "";
            }

            mapData.arrowLocations = sArray;
        }

        Manager.SetMapData(mapData);

        gameObject.SetActive(false);
    }


}
