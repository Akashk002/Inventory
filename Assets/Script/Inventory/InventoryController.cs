using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController
{
    [SerializeField] private InventoryItemList inventoryItemList;

    private Slot slot;
    private int quantity;
    private InventoryItem inventoryItem;
    private InventoryView InventoryView;

    public void OnEnable()
    {
        GameService.Instance.GetEventService().OnConfirmBuyItem.AddListener(AddItemInInventory);
        GameService.Instance.GetEventService().OnAddResourceInInventory.AddListener(AddItemInInventory);
        GameService.Instance.GetEventService().OnSellItem.AddListener(SetSellItemInfo);
    }

    public void OnDisable()
    {
        GameService.Instance.GetEventService().OnConfirmBuyItem.RemoveListener(AddItemInInventory);
        GameService.Instance.GetEventService().OnAddResourceInInventory.RemoveListener(AddItemInInventory);
        GameService.Instance.GetEventService().OnSellItem.RemoveListener(SetSellItemInfo);
    }

    public InventoryController(InventoryView inventoryView, InventoryItemList inventoryItemList)
    {
        this.InventoryView = Object.Instantiate(inventoryView);
        InventoryView.transform.SetParent(GameService.Instance.GetParentTransform(), false);
        InventoryView.transform.SetAsFirstSibling();
        InventoryView.SetInventoryController(this);
        this.inventoryItemList = inventoryItemList;
    }

    private void AddItemInInventory(InventoryItem inventoryItem, int quantity)
    {
        for (int i = 0; i < InventoryView.GetSlotLists().Count; i++)
        {
            Slot slot = InventoryView.GetSlotLists()[i];

            if (slot.GetInventoryItem() == null)
            {
                slot.AddNewItemInSlot(inventoryItem, quantity);
                break;
            }
            else
            if (slot.GetInventoryItem().Name == inventoryItem.Name)
            {
                slot.AddSameItemInSlot(quantity);
                break;
            }
        }

        int totalWeightGain = inventoryItem.weight * quantity;
        GameService.Instance.UpdateWeight(totalWeightGain);
    }

    private void SetSellItemInfo(Slot Slot, int Quanitity)
    {
        slot = Slot;
        quantity = Quanitity;
        inventoryItem = slot.GetInventoryItem();
    }

    public void SellConfirm()
    {
        if (slot && slot.GetSlotType() == SlotType.Inventory)
        {
            int totalCost = inventoryItem.sellingPrice * quantity;
            int totalWeightDec = inventoryItem.weight * quantity;

            slot.DecreaseItemQuantity(quantity);

            GameService.Instance.AddCoin(totalCost);
            GameService.Instance.GetAudioService().Play(SoundType.SellSuccess);
            GameService.Instance.GetEventService().OnConfirmSellItem.InvokeEvent(inventoryItem, quantity);
            GameService.Instance.UpdateWeight(-totalWeightDec);
            slot = null;
            quantity = 0;
        }
    }
}
