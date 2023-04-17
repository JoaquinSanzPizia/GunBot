using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class ItemSpawner : MonoBehaviour
{
    public ObjectPooler pooler;
    public ItemSO[] itemsArray;
    [SerializeField] int maxEnemyAmount;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //SpawnItem(item);
        }
    }

    void SpawnItem(int itemIndex)
    {
        pooler.SpawnFromPool("bot01", transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), transform.rotation);
    }
}

