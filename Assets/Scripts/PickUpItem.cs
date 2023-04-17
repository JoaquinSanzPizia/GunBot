using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();

        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);

            if (reminder == 0)
                item.DestroyItem(other.gameObject.transform.position);
            else
                item.Quantity = reminder;
        }
    }
}
