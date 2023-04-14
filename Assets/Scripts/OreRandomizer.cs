using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OreRandomizer : MonoBehaviour
{
    [SerializeField] SpriteRenderer editorIndicator;
    [SerializeField] GameObject[] cubes;
    [SerializeField] Sprite[] topTextures;
    [SerializeField] Sprite[] bottomTextures;

    private AstarPath path; 


    void Start()
    {
        editorIndicator.enabled = false;
        path = FindObjectOfType<AstarPath>();
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

        path.Scan();
    }
}
