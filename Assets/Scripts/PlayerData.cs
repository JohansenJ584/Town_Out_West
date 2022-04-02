using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float CointValue;
    public int LogsValue;
    public string[] InventoryList;
    public string[] EquipmentList;
    //Will Later get stats
    public PlayerData(CoinCount TempCoins, LogCount TempLogs, InventoryManager tempInventory, EquipmentManager tempEquip)
    {
        CointValue = TempCoins.GetCoinValue();
        LogsValue = TempLogs.GetLogCount();
        InventoryList = tempInventory.GetNamesInventory();
        EquipmentList = tempEquip.GetNamesEquipment();
    }

    public PlayerData()
    {

    }
}
