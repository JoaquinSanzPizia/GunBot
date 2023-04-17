using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using System;
using Inventory.UI;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] UIManager uiManager;
        [SerializeField] InventoryUI inventoryUI;

        [SerializeField] InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();
        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage,
                    item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventory(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {

        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                Debug.Log("Clicking on empty slot");
                inventoryUI.ResetSelection();
                return;
            }
            else
            {
                ItemSO item = inventoryItem.item;
                inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
            {
                Show();
            }
        }

        void Show()
        {
            uiManager.ShowInventory();

            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }
    }
}