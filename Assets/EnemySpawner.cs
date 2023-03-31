using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPooler pooler;
    public int enemieAmount;
    void Start()
    {
        SpawnEnemie();
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
        LeanTween.delayedCall(1f, () => 
        {
            pooler.SpawnFromPool("enemy", transform.position, transform.rotation);
        });
    }
}
