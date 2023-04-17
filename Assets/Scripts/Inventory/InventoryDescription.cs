using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory.UI
{
    public class InventoryDescription : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private TextMeshProUGUI description;
        [SerializeField] UIManager uIManager;


        public void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            uIManager.HideDescription();
            itemImage.gameObject.SetActive(false);
            title.text = "";
            description.text = "";
        }

        public void SetDescription(Sprite sprite, string itemName, string itemDescription)
        {
            uIManager.ShowDescription();
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            title.text = itemName;
            description.text = itemDescription;
        }
    }
}