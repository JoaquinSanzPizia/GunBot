using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] enum EnemyType { bot01, bot02}
    [SerializeField] EnemyType enemyType;
    public ObjectPooler pooler;
    public int[] enemyAmount;
    [SerializeField] int maxEnemyAmount;
    void Start()
    {
        enemyAmount[0] = 0;
        enemyAmount[1] = 0;

        TrySpawnEnemie(EnemyType.bot01);
        TrySpawnEnemie(EnemyType.bot02);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TrySpawnEnemie(EnemyType.bot01);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            TrySpawnEnemie(EnemyType.bot02);
        }
    }

    void TrySpawnEnemie(EnemyType enemy)
    {
        switch (enemy)
        {
            case EnemyType.bot01:
                pooler.SpawnFromPool("enemy01", transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), transform.rotation);
                break;

            case EnemyType.bot02:
                pooler.SpawnFromPool("enemy02", transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), transform.rotation);
                break;
        }
    }
}
