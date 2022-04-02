using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StoreManager : MonoBehaviour
{
    #region Singleton
    public static StoreManager instance;

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

    /*
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

    Dictionary<Item, float> ValueForItems = new Dictionary<Item, float>();

    Dictionary<Item, float> DictToBuy = new Dictionary<Item, float>();
    //maybe will use scriptble object to hold different shops maybe
    */
    public List<StoresList> ListOfStores = new List<StoresList>();

    public CoinCount coinUi;

    public int activeStore = 0;

    List<Item> ListOfThingsToSell = new List<Item>();
    List<SellSlot> ListofSSlots = new List<SellSlot>();
    [HideInInspector]
    public List<Item> ListOfThingsThatCanBeBought = new List<Item>();
    List<BuySlot> ListofBSlots = new List<BuySlot>();

    // Start is called before the first frame update
    void Start()
    {

        foreach (StoresList temp in ListOfStores)
        {
            temp.makeDict();
        }
    }
    public void addItem(Item item, SellSlot slot)
    {
        //Debug.Log("should be added");
        ListOfThingsToSell.Add(item);
        ListofSSlots.Add(slot);
    }
    public void removeItem(Item item, SellSlot slot)
    {
        //Debug.Log("should be removed");
        ListOfThingsToSell.Remove(item);
        ListofSSlots.Remove(slot);
    }

    public void addItemBuy(Item item, BuySlot slot)
    {
        //Debug.Log("should be added");
        ListOfThingsThatCanBeBought.Add(item);
        ListofBSlots.Add(slot);
    }
    public void removeItemBuy(Item item, BuySlot slot)
    {
        //Debug.Log("should be removed");

        ListOfThingsThatCanBeBought.Remove(item);
        ListofBSlots.Remove(slot);
    }


    public void ItemsSold()
    {
        float sellValue;
        for (int i = 0; i < ListOfThingsToSell.Count; i++)
        {
            sellValue = ListOfStores[activeStore].ValueForItems[ListOfThingsToSell[i]];
            coinUi.ChangeCoinCount(sellValue);
            //Debug.Log("should be sold");
        }
        for (int i = 0; i < ListOfThingsToSell.Count; i++)
        {
            //Debug.Log("should be removed2");
            InventoryManager.instance.Remove(ListOfThingsToSell[i]);
        }
        for (int i = 0; i < ListofSSlots.Count; i ++)
        {
            ListofSSlots[i].Selected();
        }
        ListofSSlots.Clear();
        ListOfThingsToSell.Clear();
    }

    public void ItemBought()
    {
        float BuyValue;
        bool DoesThisBreak = true; ;
        for (int i = 0; i < ListOfThingsThatCanBeBought.Count; i++)
        {
            BuyValue = ListOfStores[activeStore].DictToBuy[ListOfThingsThatCanBeBought[i]];
            if(!(coinUi.ChangeCoinCount(0.0f-BuyValue)))
            {
                DoesThisBreak = false;
                break;
            }
            //Debug.Log("should be sold");
        }
        if (DoesThisBreak)
        {
            for (int i = 0; i < ListOfThingsThatCanBeBought.Count; i++)
            {
                //Debug.Log("should be removed2");
                InventoryManager.instance.Add(ListOfThingsThatCanBeBought[i]);
            }
            for (int i = 0; i < ListofBSlots.Count; i++)
            {
                ListofBSlots[i].Selected();
            }
        }
        ListofBSlots.Clear();
        ListOfThingsThatCanBeBought.Clear();
    }
}
