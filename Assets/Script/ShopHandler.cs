using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] InventoryItemList inventoryItemList;
    [SerializeField] GridLayoutGroup material, weapon, consumable,treasure;
    Slot slot;
    int quantity;
    InventoryItem InventoryItem;

    private void OnEnable()
    {
        EventService.Instance.OnBuyItem.AddListener(SetBuyItemInfo);
        EventService.Instance.OnConfirmSellItem.AddListener(AddItemInShop);
    }

    void OnDisable()
    {
        EventService.Instance.OnBuyItem.RemoveListener(SetBuyItemInfo);
        EventService.Instance.OnConfirmSellItem.RemoveListener(AddItemInShop);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpShop();
    }

    void SetUpShop()
    {
        List<InventoryItem> materialList = inventoryItemList.MaterialsList;
        List<InventoryItem> WeaponList = inventoryItemList.WeaponsList;
        List<InventoryItem> consumableList = inventoryItemList.ConsumablesList;
        List<InventoryItem> treasureList = inventoryItemList.TreasureList;


        for (int i = 0; i < materialList.Count; i++)
        {
            material.transform.GetChild(i).GetComponent<Slot>().AddNewItemInSlot(materialList[i],materialList[i].quantity);
        } 
        
        for (int i = 0; i < WeaponList.Count; i++)
        {
            weapon.transform.GetChild(i).GetComponent<Slot>().AddNewItemInSlot(WeaponList[i],WeaponList[i].quantity);
        } 
        
        for (int i = 0; i < consumableList.Count; i++)
        {
            consumable.transform.GetChild(i).GetComponent<Slot>().AddNewItemInSlot(consumableList[i],consumableList[i].quantity);
        } 
        
        for (int i = 0; i < treasureList.Count; i++)
        {
            treasure.transform.GetChild(i).GetComponent<Slot>().AddNewItemInSlot(treasureList[i],treasureList[i].quantity);
        }

        material.gameObject.SetActive(true);
    }


    void SetBuyItemInfo(Slot Slot, int Quantity)
    {
        slot = Slot;
        quantity = Quantity;
        InventoryItem = slot.GetInventoryItem();
    }

    public void BuyConfirm()
    {
        if (slot && slot.GetSlotType() == SlotType.Shop)
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

    void AddItemInShop(InventoryItem inventoryItem,int quantity)
    {
       ItemType itemType = inventoryItem.type;

        Transform transform = null;

        switch (itemType)
        {
            case ItemType.Materials:
                transform = material.transform;
                break;
            case ItemType.Weapons:
                transform = weapon.transform;
                break;
            case ItemType.Consumables:
                transform = consumable.transform;
                break;
            case ItemType.Treasure:
                transform = treasure.transform;
                break;
            default:
                break;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Slot slot = transform.GetChild(i).GetComponent<Slot>();

            if (slot.GetInventoryItem() == null)
            {
                slot.AddNewItemInSlot(inventoryItem, quantity);
                break;
            }
            else
            if (slot.GetInventoryItem().name == inventoryItem.name)
            {
                slot.AddSameItemInSlot(quantity);
                break;
            }
        }

    }
}

