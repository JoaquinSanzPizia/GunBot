using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPooler pooler;
    public int enemieAmount;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnEnemie();
        }
    }

    void SpawnEnemie()
    {
        pooler.SpawnFromPool("enemy", transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), transform.rotation);
    }
}
