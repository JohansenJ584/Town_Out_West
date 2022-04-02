using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Equipment[] currentEquipment;
    InventoryManager inventoryObject;
    public Transform itemsParent;
    EquipmentSlot[] slots;

    public Equipment[] StartingGear = new Equipment[8];  //look look

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public event OnEquipmentChanged onEquipmentChanged;

    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentEnum)).Length;
        currentEquipment = new Equipment[numSlots];
        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < StartingGear.Length; i++)
        {
            int slotIdex = (int)StartingGear[i].equipSlot;
            currentEquipment[slotIdex] = StartingGear[i];
        }

        WeoponManager.instance.EquipingAndDequiping();

        UpdateUi();
        inventoryObject = InventoryManager.instance;
        Invoke("LateStartEquip", .01f);
    }

    void LateStartEquip()
    {
        for (int i = 0; i < StartingGear.Length; i++)
        {
            Equip(StartingGear[i]);
            //Debug.Log("oone");
        }
    }

    public void Equip (Equipment newItem)
    {
        int slotIdex = (int)newItem.equipSlot;
        ////this is for player stats;
        Equipment oldItem = currentEquipment[slotIdex];
        //
        if (currentEquipment[slotIdex] != null)
        {
            inventoryObject.Add(currentEquipment[slotIdex]); //This is new
        }
        //this is for player stats;
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);
        //

        currentEquipment[slotIdex] = newItem;
        //inventoryObject.Add
        inventoryObject.Remove(newItem); //NEW
        UpdateUi();
        if (newItem != oldItem)
        {
            WeoponManager.instance.EquipingAndDequiping();
        }
    }
    public void Remove(Equipment item)
    {
        if (inventoryObject.Add(item))
        {
            currentEquipment[(int)item.equipSlot] = null;
            UpdateUi();
        }
    }

    void UpdateUi()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (currentEquipment[i] != null)
            {
                slots[i].AddItem(currentEquipment[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public string[] GetNamesEquipment()
    {
        string[] EquipmentArray = new string[currentEquipment.Length];
        int count = 0;
        foreach (Item tempEquipment in currentEquipment)
        {
            EquipmentArray[count] = tempEquipment.Itemname;
            count++;
        }
        return EquipmentArray;
    }
}
