using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryHandler : GenericMonoSingleton<InventoryHandler>
{
    [SerializeField] private InventoryItemList inventoryItemList;
    [SerializeField] private GridLayoutGroup inventory;
    [SerializeField] private TMP_Text weightText;
    [SerializeField] private int totalSlot;
    [SerializeField] private int maxWeight;
    [SerializeField] private int numOfTypesOfItem = 4;

    private int currentWeight;
    private Slot slot;
    private int quantity;
    private InventoryItem inventoryItem;

    private void OnEnable()
    {
        EventService.Instance.OnConfirmBuyItem.AddListener(AddItemInInventory);
        EventService.Instance.OnSellItem.AddListener(SetSellItemInfo);
    }

    private void OnDisable()
    {
        EventService.Instance.OnConfirmBuyItem.RemoveListener(AddItemInInventory);
        EventService.Instance.OnSellItem.RemoveListener(SetSellItemInfo);
    }

    private void UpdateWeight(int weight)
    {
        currentWeight += weight;
        weightText.text = $"{currentWeight}/{maxWeight}";
    }

    private void AddItemInInventory(InventoryItem inventoryItem,int quantity)
    {
        for (int i = 0; i < totalSlot; i++)
        {
            Slot slot = GameService.Instance.GetSlotLists(SlotType.Inventory)[i];

            if (slot.GetInventoryItem() == null)
            {
                slot.AddNewItemInSlot(inventoryItem, quantity);
                break;
            }
            else
            if(slot.GetInventoryItem().Name == inventoryItem.Name)
            {
                slot.AddSameItemInSlot(quantity);
                break;
            }
        }

        int totalWeightGain = inventoryItem.weight * quantity;
        UpdateWeight(totalWeightGain);
    }
    public int GetRemainingWeight()
    {
        return maxWeight - currentWeight;
    }

    public void AddResourse()
    {
        ItemType randomItemType = (ItemType)Random.Range(0, numOfTypesOfItem) ;

        List<InventoryItem> invenitoryItemList = inventoryItemList.InventoryItemScriptableList.Find(list => list.itemType == randomItemType).itemScriptableList;

        int randomItem = Random.Range(0, invenitoryItemList.Count);
        InventoryItem inventoryItem = invenitoryItemList[randomItem];

        if (inventoryItem.weight > GetRemainingWeight())
        { 
            EventService.Instance.OnBuyFailed.InvokeEvent(BuyFailedType.InventoryWeight);
            return;
        }

        AudioManager.Instance.Play(SoundType.AddResource);
        AddItemInInventory(inventoryItem,1);
    }

    private void SetSellItemInfo(Slot Slot,int Quanitity)
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
            CurrencyHandler.Instance.AddCoin(totalCost);
            slot.DecreaseItemQuantity(quantity);

            AudioManager.Instance.Play(SoundType.SellSuccess);
            EventService.Instance.OnConfirmSellItem.InvokeEvent(inventoryItem, quantity);

            UpdateWeight(-totalWeightDec);
            slot = null;
            quantity = 0;
            
        }
    }
}
