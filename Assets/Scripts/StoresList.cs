using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Store", menuName = "Level/Store")]
public class StoresList : ScriptableObject
{
    [SerializeField]
    List<Item> vkeys = new List<Item>();
    [SerializeField]
    List<float> vvalues = new List<float>();
    public List<Item> VKeys { get => vkeys; set => vkeys = value; }
    public List<float> VValues { get => vvalues; set => vvalues = value; }

    [SerializeField]
    List<Item> bkeys = new List<Item>();
    [SerializeField]
    List<float> bvalues = new List<float>();
    //[SerializeField]
    public List<Item> BKeys { get => bkeys; set => bkeys = value; }
    //[SerializeField]
    public List<float> BValues { get => bvalues; set => bvalues = value; }

    public Dictionary<Item, float> ValueForItems = new Dictionary<Item, float>();

    public Dictionary<Item, float> DictToBuy = new Dictionary<Item, float>();

    public void makeDict()
    {
        ValueForItems.Clear();
        DictToBuy.Clear();
        for (int i = 0; i < VKeys.Count; i++)
        {
            ValueForItems.Add(VKeys[i], VValues[i]);
        }

        for (int k = 0; k < BKeys.Count; k++)
        {
            DictToBuy.Add(BKeys[k], BValues[k]);
        }
    }

    void Awake()
    {
        ValueForItems.Clear();
        DictToBuy.Clear();
        for (int i = 0; i < VKeys.Count; i++)
        {
            ValueForItems.Add(VKeys[i], VValues[i]);
        }

        for (int k = 0; k < BKeys.Count; k++)
        {
            DictToBuy.Add(BKeys[k], BValues[k]);
        }
    }
}
