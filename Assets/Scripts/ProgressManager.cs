using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager {

    List<int> characterProgress = new List<int>(System.Enum.GetNames(typeof(Characters)).Length);

    public ProgressManager()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(Characters)).Length ; i++)
        {
            characterProgress.Add(0);
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

}
