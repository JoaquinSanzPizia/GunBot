using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
    }

    public void DestroyItem(Vector3 movePos)
    {
        GetComponent<Collider2D>().enabled = false;

        LeanTween.move(gameObject, movePos, 0.2f);
        LeanTween.scale(gameObject, Vector3.zero, 0.2f).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
