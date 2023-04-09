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
        int randomEnemy = Random.Range(0, 2);

        switch (randomEnemy)
        {
            case 0:
                pooler.SpawnFromPool("enemy01", transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), transform.rotation);
                break;

            case 1:
                pooler.SpawnFromPool("enemy02", transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), transform.rotation);
                break;
        }
    }
}
