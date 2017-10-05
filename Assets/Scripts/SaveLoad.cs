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
        Load();

        ProgressManager.current.date = System.DateTime.Today.ToString();

        Debug.Log(ProgressManager.current.date);

        //Debug.Log(Application.dataPath);
        if (ProgressManager.current.index == -1)
        {
            ProgressManager.current.index = savedGames.Count;
            savedGames.Add(ProgressManager.current);
        }
        else
        {
            if (savedGames.Count < ProgressManager.current.index)
            {
            savedGames[ProgressManager.current.index] = ProgressManager.current;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/savedGames.gd");
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/savedGames.gd", FileMode.Open);
            SaveLoad.savedGames = (List<ProgressManager>)bf.Deserialize(file);
            file.Close();
        }
    }
}