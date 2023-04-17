using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] ItemSpawner itemSpawner;
    [SerializeField] List<ItemToDrop> itemsToDrop;
    [SerializeField] int maxAmountToDrop;

    private void Start()
    {
        itemSpawner = FindObjectOfType<ItemSpawner>();
    }

    [System.Serializable]
    public class ItemToDrop
    {
        public ItemSO itemSO;
        public int dropChance;
    }

    public void DropItem()
    {
        for (int i = 0; i < maxAmountToDrop; i++)
        {
            int randomItem = Random.Range(0, 100);
            Vector3 randomPos = transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
            if (randomItem > itemsToDrop[0].dropChance)
            {
                itemSpawner.SpawnItem(itemsToDrop[1].itemSO, 1, randomPos);
            }
            else
            {
                itemSpawner.SpawnItem(itemsToDrop[0].itemSO, 1, randomPos);
            }
        }
    }
}
