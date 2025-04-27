using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler 
{
    [SerializeField] private InventoryItemList inventoryItemList;
    [SerializeField] private GridLayoutGroup materialPanel;
    private Slot slot;
    private int quantity;
    private InventoryItem InventoryItem;

    private void OnEnable()
    {
        EventService.Instance.OnBuyItem.AddListener(SetBuyItemInfo);
        EventService.Instance.OnConfirmSellItem.AddListener(AddItemInShop);
    }

    private void OnDisable()
    {
        EventService.Instance.OnBuyItem.RemoveListener(SetBuyItemInfo);
        EventService.Instance.OnConfirmSellItem.RemoveListener(AddItemInShop);
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetUpShop();
    }

    private void SetUpShop()
    {
        for (int i = 0; i < 4; i++)
        {
            List<InventoryItem> itemList = inventoryItemList.InventoryItemScriptableList[i].itemScriptableList;
            List<Slot> slotList =  GameService.Instance.GetSlotLists((SlotType)i); 
            for (int j = 0; j < slotList.Count; j++)
            {
                slotList[j].AddNewItemInSlot(itemList[j], itemList[j].quantity);
            }
        }
        materialPanel.gameObject.SetActive(true);
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

            if (totalCost > CurrencyHandler.Instance.GetCoin())
            {
                EventService.Instance.OnBuyFailed.InvokeEvent(BuyFailedType.Coin);
                slot = null;
                quantity = 0;
                return;
            }
            
            if (totalWeight > InventoryHandler.Instance.GetRemainingWeight())
            {
                EventService.Instance.OnBuyFailed.InvokeEvent(BuyFailedType.InventoryWeight);
                slot = null;
                quantity = 0;
                return;
            }

            AudioManager.Instance.Play(SoundType.BuySuccess);
            CurrencyHandler.Instance.SpentCoin(totalCost);
            slot.DecreaseItemQuantity(quantity);

            EventService.Instance.OnConfirmBuyItem.InvokeEvent(InventoryItem, quantity);
            slot = null;
            quantity = 0;
        }
    }

    private void AddItemInShop(InventoryItem inventoryItem,int quantity)
    {
        ItemType itemType = inventoryItem.type;
        List<Slot> slotList = GameService.Instance.GetSlotLists((SlotType)itemType);

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

