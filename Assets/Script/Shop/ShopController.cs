using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController
{
    private InventoryItemList inventoryItemList;
    private ShopView shopView;
    private Slot slot;
    private int quantity;
    private InventoryItem InventoryItem;
    public ShopController(ShopView shopPrefab, InventoryItemList inventoryItemList)
    {
        this.shopView = Object.Instantiate(shopPrefab);
        shopView.transform.SetParent(GameService.Instance.GetParentTransform().transform, false);
        shopView.transform.SetAsFirstSibling();
        shopView.SetShopController(this);
        this.inventoryItemList = inventoryItemList;
        SetUpShop();

    }

    public void OnEnable()
    {
        GameService.Instance.GetEventService().OnBuyItem.AddListener(SetBuyItemInfo);
        GameService.Instance.GetEventService().OnConfirmSellItem.AddListener(AddItemInShop);
    }

    public void OnDisable()
    {
        GameService.Instance.GetEventService().OnBuyItem.RemoveListener(SetBuyItemInfo);
        GameService.Instance.GetEventService().OnConfirmSellItem.RemoveListener(AddItemInShop);
    }


    private void SetUpShop()
    {
        for (int i = 0; i < 4; i++)
        {
            List<InventoryItem> itemList = inventoryItemList.InventoryItemScriptableList[i].itemScriptableList;
            List<Slot> slotList = GetSlotLists((SlotType)i);
            for (int j = 0; j < slotList.Count; j++)
            {
                slotList[j].AddNewItemInSlot(itemList[j], itemList[j].quantity);
            }
        }
        shopView.GetmaterialPanel().gameObject.SetActive(true);
    }

    public List<Slot> GetSlotLists(SlotType slotType)
    {
        List<Slot> SlotList = shopView.GetSlotLists().Find(list => list.slotType == slotType).slotList;

        return SlotList;
    }

    private void ResetSlot()
    {
        slot = null;
        quantity = 0;
        InventoryItem = null;
    }

    private void SetBuyItemInfo(Slot Slot, int Quantity)
    {
        slot = Slot;
        quantity = Quantity;
        InventoryItem = slot.GetInventoryItem();
    }

    public void BuyConfirm()
    {
        if (slot && slot.GetSlotType() != SlotType.Inventory)
        {
            int totalCost = InventoryItem.buyingPrice * quantity;
            int totalWeight = InventoryItem.weight * quantity;

            if (totalCost > GameService.Instance.GetCoin())
            {
                GameService.Instance.GetEventService().OnBuyFailed.InvokeEvent(BuyFailedType.Coin);
                ResetSlot();
                return;
            }

            if (totalWeight > GameService.Instance.GetRemainingWeight())
            {
                GameService.Instance.GetEventService().OnBuyFailed.InvokeEvent(BuyFailedType.InventoryWeight);
                ResetSlot();
                return;
            }

            GameService.Instance.GetAudioService().Play(SoundType.BuySuccess);
            GameService.Instance.SpentCoin(totalCost);
            slot.DecreaseItemQuantity(quantity);

            GameService.Instance.GetEventService().OnConfirmBuyItem.InvokeEvent(InventoryItem, quantity);
            ResetSlot();
        }
    }

    private void AddItemInShop(InventoryItem inventoryItem, int quantity)
    {
        ItemType itemType = inventoryItem.type;
        List<Slot> slotList = GetSlotLists((SlotType)itemType);

        for (int i = 0; i < slotList.Count; i++)
        {
            Slot slot = slotList[i];
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

    }
}
