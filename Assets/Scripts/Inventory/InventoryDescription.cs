using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;


    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        itemImage.gameObject.SetActive(false);
        title.text = "";
        description.text = "";
    }

    public void SetDescription(Sprite sprite, string itemName, string itemDescription)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        title.text = itemName;
        description.text = itemDescription;
    }
}
