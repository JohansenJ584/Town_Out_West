using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellSlot : MonoBehaviour
{
    public Button theButton;
    public Image icon;
    Item item;
    bool isClicked = false;

    private void OnEnable()
    {
        theButton.image.color = (Color.gray);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        Selected();
       
    }
    public void Selected()
    {
        if (!isClicked && item != null)
        {
            theButton.image.color = Color.red;
            StoreManager.instance.addItem(item, this);
            isClicked = true;
        }
        else
        {
            theButton.image.color = (Color.gray);
            StoreManager.instance.removeItem(item, this);
            isClicked = false;
        }
    }
}
