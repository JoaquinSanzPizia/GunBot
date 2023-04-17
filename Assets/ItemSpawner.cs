using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class ItemSpawner : MonoBehaviour
{
    public ObjectPooler pooler;
    public void SpawnItem(ItemSO itemSO, int quantity, Vector3 spawnPos)
    {
        GameObject item = pooler.SpawnFromPool("item", spawnPos, Quaternion.identity);
        item.GetComponent<Item>().InventoryItem = itemSO;
        item.GetComponent<Item>().Spawn();
        item.GetComponent<Item>().Quantity = quantity;
    }
}
