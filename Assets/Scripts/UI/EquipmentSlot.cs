using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : InventorySlot
{
    //ForNownothing
    public override void OnRemoveButton()
    {
        EquipmentManager.instance.Remove((Equipment)item);
    }
}
