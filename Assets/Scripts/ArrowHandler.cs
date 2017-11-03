using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Directions
{
    up, down, left, right
}

public class ArrowHandler : MonoBehaviour {

    GameObject[] arrows;

    string[] currentLocations = new string[4];

    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;

    void Start()
    {
        arrows = new GameObject[4];

        arrows[0] = upArrow;
        arrows[1] = downArrow;
        arrows[2] = leftArrow;
        arrows[3] = rightArrow;

        foreach (GameObject g in arrows)
        {
            g.SetActive(false);
        }
    }

    public void SetActive(bool value, bool showEffects)
    {
        gameObject.SetActive(value);

        UpdateArrows(currentLocations, showEffects);
    }

    public void UpdateArrows(string[] locations, bool showEffects)
    {
        for (int i = 0; i < 4; i++)
        {
            currentLocations[i] = locations[i];

            if( locations[i] == "")
            {
                arrows[i].SetActive(false);
            }
            else
            {
                arrows[i].SetActive(true);
                arrows[i].GetComponent<TransitionTrigger>().SetText(locations[i]);

                if (showEffects)
                {
                    arrows[i].GetComponentInChildren<ParticleSystem>().Play();
                }
            }
        }
    }
}
