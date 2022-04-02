using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    #region Singleton
    public static InventoryManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There are more than one inventory managers");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallBack;

    public int space = 20;
    public List<Item> items = new List<Item>();

    public List<Item> ReferenceForAllItems = new List<Item>();//USed to load inventory

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Ran out of space");
            return false;
        }
        items.Add(item);
        if(onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

    public string[] GetNamesInventory()
    {
        string[] ItemsArray = new string[items.Count];
        int count = 0;
        foreach(Item tempItem in items)
        {
            ItemsArray[count] = tempItem.name;
            count++;
        }
        return ItemsArray;
    } 
}
