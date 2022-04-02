using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void CompleteWipe()
    {
        string path1 = Application.persistentDataPath + "/player.data";

        //Town
        string path2 = Application.persistentDataPath + "/TownData.data";

        try
        {
            File.Delete(path1);
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }

        try
        {
            File.Delete(path2);
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }

    }

    public static void SaveTownBetweenScenes()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/TownData.data";
        FileStream stream2 = new FileStream(path, FileMode.Create);
        TownData townData = new TownData();

        formatter.Serialize(stream2, townData);
        stream2.Close();
    }

    public static void LoadTownBetweenScenes()
    {
        string path2 = Application.persistentDataPath + "/TownData.data";
        if (File.Exists(path2))
        {
            BinaryFormatter formatter2 = new BinaryFormatter();
            FileStream stream2 = new FileStream(path2, FileMode.Open);
            TownData data2 = formatter2.Deserialize(stream2) as TownData;
            stream2.Close();
            TownData.LoadValues(data2.AmountOfLogs);
            //Debug.Log(data2.AmountOfLogs[0]);
        }
    }



    //Should Save the Data
    public static void SaveToFiles(CoinCount TempCoins, LogCount TempLogs, InventoryManager tempInventory, EquipmentManager tempEquip) //What do I put in here
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(TempCoins, TempLogs, tempInventory, tempEquip);

        formatter.Serialize(stream, data);
        stream.Close();

        //Town
        path = Application.persistentDataPath + "/TownData.data";
        FileStream stream2 = new FileStream(path, FileMode.Create);
        TownData townData = new TownData();

        formatter.Serialize(stream2, townData);
        stream2.Close();
    }

    public static PlayerData LoadFromFiles()
    {
        string path2 = Application.persistentDataPath + "/TownData.data";
        if (File.Exists(path2))
        {
            BinaryFormatter formatter2 = new BinaryFormatter();
            FileStream stream2 = new FileStream(path2, FileMode.Open);
            TownData data2 = formatter2.Deserialize(stream2) as TownData;
            stream2.Close();
            TownData.LoadValues(data2.AmountOfLogs);
            //Debug.Log(data2.AmountOfLogs[0]);
        }

        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            //Debug.Log("Coins:  "+ data.CointValue + "\nLogCount:  " + data.LogsValue + "\nList Items:  " + data.InventoryList[0] + "\nEquip List:  " + data.EquipmentList[0]);
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file name not found:   " + path);
            return null;
        }
    }

    public static void SaveTown()
    {

    }
}
