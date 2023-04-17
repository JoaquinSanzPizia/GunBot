using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer editorIndicator;

    [SerializeField] int damageResistance;
    [SerializeField] int maxHp;
    [SerializeField] int currentHp;
    [SerializeField] GameObject[] models;

    public ItemDropper itemDropper;
    public bool dropsItem;

    GameObject currentModel;
    [SerializeField] ParticleSystem breakFX;
    PolygonCollider2D col;

    void Start()
    {
        col = gameObject.GetComponent<PolygonCollider2D>();
        col.enabled = true;
        currentHp = maxHp;
        currentModel = models[Random.Range(0, models.Length)];
        currentModel.SetActive(true);
        editorIndicator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GetHit();
        }
    }

    void Respawn()
    {
        col.enabled = true;
        currentHp = maxHp;
        currentModel = models[Random.Range(0, models.Length)];
        currentModel.SetActive(true);
    }

    void GetHit()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, 0f);

        currentHp--;
        LeanTween.scale(gameObject, gameObject.transform.localScale * 1.1f, 0.1f).setLoopPingPong(1);

        if (currentHp <= 0)
        {
            Break();
        }
    }

    void Break()
    {
        breakFX.Play();
        currentModel.SetActive(false);
        col.enabled = false;

        if (dropsItem)
        {
            itemDropper.DropItem();
        }
    }
}
