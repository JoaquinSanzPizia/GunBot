using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] UiInventoryItem itemPrefab;
        [SerializeField] RectTransform contentPanel;
        [SerializeField] InventoryDescription inventoryDescription;
        [SerializeField] MouseFollower mouseFollower;

        List<UiInventoryItem> itemList = new List<UiInventoryItem>();

        public int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested,
                       OnItemActionRequested,
                       OnStartDragging;

        public event Action<int, int> OnSwapItems;

        public void InitializeInventory(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UiInventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                item.transform.SetParent(contentPanel);
                itemList.Add(item);
                item.OnItemClicked += HandleItemSelection;
                item.OnItemBeginDrag += HandleBeginDrag;
                item.OnItemDroppedOn += HandleSwap;
                item.OnItemEndDrag += HandleEndDrag;
                item.OnRightMouseButtonClick += HandleShowItemActions;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ResetSelection();
                ResetDraggedItem();
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            inventoryDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            itemList[itemIndex].Select();
        }

        public void ResetSelection()
        {
            inventoryDescription.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UiInventoryItem item in itemList)
            {
                item.Deselect();
            }
        }

        private void Awake()
        {
            ResetSelection();
        }

        internal void ResetAllItems()
        {
            foreach (var item in itemList)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemAmount)
        {
            if (itemList.Count > itemIndex)
            {
                itemList[itemIndex].SetData(itemImage, itemAmount);
            }
        }
        private void HandleShowItemActions(UiInventoryItem inventoryItem)
        {
            throw new NotImplementedException();
        }

        private void HandleEndDrag(UiInventoryItem inventoryItem)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UiInventoryItem inventoryItem)
        {
            int index = itemList.IndexOf(inventoryItem);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelection(inventoryItem);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UiInventoryItem inventoryItem)
        {
            int index = itemList.IndexOf(inventoryItem);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItem);
            OnStartDragging?.Invoke(index);

        }

        public void CreateDraggedItem(Sprite sprite, int amount)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, amount);
        }

        private void HandleItemSelection(UiInventoryItem inventoryItem)
        {
            int index = itemList.IndexOf(inventoryItem);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }
    }
}