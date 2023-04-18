using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UiInventoryItem : MonoBehaviour
    {
        [SerializeField] public enum ItemType { none, speedCard, core, trinket, storage, battery, light, helmet, armor, boots}
        [SerializeField] public ItemType itemType;
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI amountText;
        [SerializeField] Image borderImage;

        public event Action<UiInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseButtonClick;

        public bool isSpecialSlot;
        public bool empty = true;
        public void Awake()
        {
            ResetData();
            Deselect();
        }
        public void ResetData()
        {
            amountText.text = "";
            itemImage.gameObject.SetActive(false);
            empty = true;
        }
        public void Deselect()
        {
            borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            amountText.text = quantity + "";
            empty = false;
        }
        public void Select()
        {
            borderImage.enabled = true;
        }
        public void OnPointerClick(BaseEventData data)
        {
            if (empty)
                return;
            PointerEventData pointerData = (PointerEventData)data;
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseButtonClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnEndDrag()
        {
            OnItemEndDrag?.Invoke(this);
            itemImage.enabled = true;
            amountText.enabled = true;
        }

        public void OnBeginDrag()
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
            itemImage.enabled = false;
            amountText.enabled = false;
        }

        public void OnDrop()
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}