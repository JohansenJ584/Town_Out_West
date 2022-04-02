using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public GameObject prefabEquipment;
    public EquipmentEnum equipSlot;
    public float armorModifier;
    public float speedModifier;
    public float damageModifier;
    public float rangeModifier;
    public float gatheringModifier;
    public float jumpModifier;
    //armoure

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
    }
}

public enum EquipmentEnum {FirstSlot, SecondSlot, ThirdSlot, Arrow, Helmet, ChestPlate, Leggins, Boots}; //Will add more later