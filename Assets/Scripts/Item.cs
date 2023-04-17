using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    public void Spawn()
    {
        GetComponent<Collider2D>().enabled = true;
        LeanTween.moveLocalX(gameObject, gameObject.transform.position.x + Random.Range(-0.1f, 0.1f), 0.3f);
        LeanTween.moveLocalY(gameObject, gameObject.transform.position.y + 0.2f, 0.15f).setLoopPingPong(1);
        gameObject.transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, 0.15f).setEaseOutBack();
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
        LeanTween.moveLocalY(gameObject, gameObject.transform.position.y + 0.05f, 0.7f).setLoopPingPong();
    }

    public void DestroyItem(Vector3 movePos)
    {
        GetComponent<Collider2D>().enabled = false;

        LeanTween.move(gameObject, movePos, 0.2f);
        LeanTween.scale(gameObject, Vector3.zero, 0.2f).setOnComplete(() => 
        {
            LeanTween.cancel(gameObject);
        });
    }
}
