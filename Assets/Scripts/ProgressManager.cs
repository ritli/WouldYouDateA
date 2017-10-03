﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressManager {

    public static ProgressManager current;

    List<int> characterProgress = new List<int>(System.Enum.GetNames(typeof(Characters)).Length);
    List<bool> characterLeft = new List<bool>(System.Enum.GetNames(typeof(Characters)).Length);
    List<bool> characterAngry = new List<bool>(System.Enum.GetNames(typeof(Characters)).Length);

    public ProgressManager()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(Characters)).Length ; i++)
        {
            characterProgress.Add(0);
            characterLeft.Add(false);
            characterAngry.Add(false);
        }
    }

    public void AddProgress(int index, int count)
    {
        characterProgress[index] += count;
    }

    public int GetProgress(int index)
    {
        return characterProgress[index];
    }

    public void SetCharacterLeft(int index)
    {
        characterLeft[index] = true;
    }

    public bool GetCharacterLeft(int index)
    {
        return characterLeft[index];
    }

    public void SetCharacterAngry(int index)
    {
        characterLeft[index] = true;
    }

    public bool GetCharacterAngry(int index)
    {
        return characterLeft[index];
    }


    public void ResetLeave()
    {
        for (int i = 0; i < characterLeft.Count; i++)
        {
            characterLeft[i] = false;
        }
    }



}
