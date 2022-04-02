using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    public Transform itemsParent;
    InventoryManager inventoryObject;
    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventoryObject = InventoryManager.instance;
        inventoryObject.onItemChangedCallBack += UpdateUi;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateUi()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventoryObject.items.Count)
            {
                slots[i].AddItem(inventoryObject.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
