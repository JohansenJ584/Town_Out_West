using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{

    public Button theButton;
    public Text ValueCost;
    public Image icon;

    Item item;
    bool isClicked = false;
    /// <summary>
    /// Adds a Item to a button allowing it to be displayed
    /// </summary>
    /// <param name="newItem">The new item to be add to the Button</param>
    /// <param name="ItemCost">Cost of said button</param>
    public void AddItem(Item newItem, float ItemCost)
    {
        item = newItem;
        ValueCost.text = ItemCost.ToString();
        icon.sprite = item.icon;
        icon.enabled = true;
    }
    /// <summary>
    /// Selects and Deslects button to be included in buying list
    /// </summary>
    public void Selected()
    {
        //Debug.Log("Selected");
        if (!isClicked && item != null)
        {
            theButton.image.color = Color.red;
            StoreManager.instance.addItemBuy(item, this);
            isClicked = true;
            //Debug.Log("Going Red");
        }
        else
        {
            theButton.image.color = (Color.gray);
           //StoreManager.instance.removeItemBuy(item, this);
            isClicked = false;
           // Debug.Log("Going Dark");
        }
    }
}
