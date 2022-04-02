using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;

        //DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        //Application.targetFrameRate = 60;
    }
    #endregion 


    public GameObject player;
    private static PlayerManager playerInstance;

    CoinCount cCount;
    LogCount lCount;
    InventoryManager iManger;
    EquipmentManager eManger;

    public void WipeSaves()
    {
        SaveSystem.CompleteWipe();
    }

    public void SaveData()
    {
        cCount = GameObject.Find("Coin Count").GetComponent<CoinCount>();
        lCount = GameObject.Find("Log Count").GetComponent<LogCount>();
        iManger = gameObject.GetComponent<InventoryManager>();
        eManger = gameObject.GetComponent<EquipmentManager>();
        Debug.Log("Data Saved");
        SaveSystem.SaveToFiles(cCount, lCount, iManger, eManger);
         
    }
    public void LoadData()
    {
        cCount = GameObject.Find("Coin Count").GetComponent<CoinCount>();
        lCount = GameObject.Find("Log Count").GetComponent<LogCount>();
        iManger = gameObject.GetComponent<InventoryManager>();
        eManger = gameObject.GetComponent<EquipmentManager>();
        Debug.Log("Data Loaded");
        //PlayerData data = 
        PlayerData data = SaveSystem.LoadFromFiles();
        cCount.ChangeCoinCount(data.CointValue); //Is this plus or minus
        lCount.ChangeLogCount(data.LogsValue);

        foreach(string tempName in data.InventoryList)
        {
            foreach(Item tempItem in iManger.ReferenceForAllItems)
            {
                //Debug.Log(tempItem.name + " |Compared| " + tempName);
                if (tempItem.name.Equals(tempName))
                {
                    iManger.Add(tempItem);
                }
            }
        }

    }
}
