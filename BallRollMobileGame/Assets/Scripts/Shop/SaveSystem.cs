/*****************************************************************************
// File Name :         SaveSystem.cs
// Author :            Kyle Grenier
// Creation Date :     07/06/2021
//
// Brief Description : Saves and loads all player data to/from disk.
*****************************************************************************/
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private const string SAVE_PATH = "/playerdata.save";

    public static void SavePlayerData(ShopDatabase purchasedItems)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVE_PATH;

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(purchasedItems);

        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + SAVE_PATH;

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found at path '" + path + "'.");
            return null;
        }
    }

    public static void ClearData()
    {
        string path = Application.persistentDataPath + SAVE_PATH;
        if (File.Exists(path))
            File.Delete(path);

        Debug.Log("Player Data deleted");
    }
}
