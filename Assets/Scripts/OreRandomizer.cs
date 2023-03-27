using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreRandomizer : MonoBehaviour
{
    [SerializeField] GameObject[] cubes;
    [SerializeField] Sprite[] topTextures;
    [SerializeField] Sprite[] bottomTextures;


    void Start()
    {
        GenerateOre();
    }

    void Update()
    {
        
    }

    void GenerateOre()
    {
        int amount = Random.Range(0, cubes.Length);

        for (int i = 0; i < amount; i++)
        {
            cubes[i].SetActive(true);
            cubes[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = topTextures[Random.Range(0, topTextures.Length)];
            cubes[i].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = bottomTextures[Random.Range(0, bottomTextures.Length)];
        }
    }
}
