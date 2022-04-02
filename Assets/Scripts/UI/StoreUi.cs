using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUi : MonoBehaviour
{
    public Transform sellItemsParent;
    public Transform buyItemsParent;
    InventoryManager inventoryObject;
    StoreManager StoreObjects;
    SellSlot[] SSlots;

    BuySlot[] BSlots;

    // Start is called before the first frame update
    void Start()
    {
        inventoryObject = InventoryManager.instance;
        inventoryObject.onItemChangedCallBack += UpdateUi;

        SSlots = sellItemsParent.GetComponentsInChildren<SellSlot>();

        BSlots = buyItemsParent.GetComponentsInChildren<BuySlot>();
        //for (int i = 0; i < BSlots.Length; i++)
        //{
        //    BSlots[i].AddItem(StoreManager.instance.BKeys[i], StoreManager.instance.BValues[i]);
        //}
    }

    public void WhatItems(int value)
    {
        for (int i = 0; i < BSlots.Length; i++)
        {
            BSlots[i].AddItem(StoreManager.instance.ListOfStores[value].BKeys[i], StoreManager.instance.ListOfStores[value].BValues[i]);
        }
    }

    void UpdateUi()
    {
        for (int i = 0; i < SSlots.Length; i++)
        {
            SSlots[i].ClearSlot();
            if (i < inventoryObject.items.Count)
            {
                SSlots[i].AddItem(inventoryObject.items[i]);
            }
            else
            {
                SSlots[i].ClearSlot();
            }
        }

    }
}
