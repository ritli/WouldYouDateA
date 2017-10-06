using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static List<ProgressManager> savedGames = new List<ProgressManager>();

    public static void Save()
    {
        if (ProgressManager.current != null)
        {
            if (savedGames.Count > ProgressManager.current.index)
            {
                savedGames[ProgressManager.current.index] = ProgressManager.current;
            }
            else
            {
                savedGames.Add(ProgressManager.current);
            }
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveLoad.savedGames = (List<ProgressManager>)bf.Deserialize(file);
            file.Close();
        }
    }
}