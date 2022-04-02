using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public void PickUp()
    {
        //Debug.Log("picking up " + item.name);
        if(InventoryManager.instance.Add(item))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 20f);
    }
}
