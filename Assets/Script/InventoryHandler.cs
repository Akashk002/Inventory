using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryHandler : MonoBehaviour
{
    private static InventoryHandler instance;
    public static InventoryHandler Instance { get { return instance; } }

    [SerializeField] InventoryItemList inventoryItemList;
    [SerializeField] GridLayoutGroup inventory;
    [SerializeField] TMP_Text weightText;
    [SerializeField] int totalSlot;
    [SerializeField] int maxWeight;
    int currentWeight;
    Slot slot;
    int quantity;
    InventoryItem inventoryItem;

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

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            UpdateWeight();
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    void UpdateWeight()
    {
        currentWeight = 0;
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            Slot slotView = inventory.transform.GetChild(i).GetComponent<Slot>();
            if (slotView.GetInventoryItem() != null)
            {
                currentWeight += slotView.GetInventoryItem().weight * slotView.GetItemQuantity();
            }
        }
        weightText.text = $"{currentWeight}/{maxWeight}";
    }

    void AddItemInInventory(InventoryItem inventoryItem,int quantity)
    {
        for (int i = 0; i < totalSlot; i++)
        {
            Slot slot = inventory.transform.GetChild(i).GetComponent<Slot>();

            if (slot.GetInventoryItem() == null)
            {
                slot.AddNewItemInSlot(inventoryItem, quantity);
                break;
            }
            else
            if(slot.GetInventoryItem().name == inventoryItem.name)
            {
                slot.AddSameItemInSlot(quantity);
                break;
            }
        }
        UpdateWeight();
    }
    public int GetRemainingWeight()
    {
        return maxWeight - currentWeight;
    }

    public void AddResourse()
    {
        ItemType randomItemType = (ItemType)Random.Range(0,4) ;
        int randomItem = Random.Range(0,12) ;

        InventoryItem inventoryItem = inventoryItemList.MaterialsList[randomItem];

        if (inventoryItem.weight > GetRemainingWeight())
        { 
            EventService.Instance.OnBuyFailed.InvokeEvent(BuyFailedType.InventoryWeight);
            return;
        }

        switch (randomItemType)
        {
            case ItemType.Weapons:
                inventoryItem = inventoryItemList.WeaponsList[randomItem];
                break;
            case ItemType.Consumables:
                inventoryItem = inventoryItemList.ConsumablesList[randomItem];
                break;
            case ItemType.Treasure:
                inventoryItem = inventoryItemList.TreasureList[randomItem];
                break;
            default:
                break;
        }

        AudioManager.Instance.Play(SoundType.AddResource);
        AddItemInInventory(inventoryItem,1);
    }

    void SetSellItemInfo(Slot Slot,int Quanitity)
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
            CurrencyHandler.Instance.AddCoin(totalCost);
            slot.DecreaseItemQuantity(quantity);

            AudioManager.Instance.Play(SoundType.SellSuccess);
            EventService.Instance.OnConfirmSellItem.InvokeEvent(inventoryItem, quantity);

            UpdateWeight();
            slot = null;
            quantity = 0;
            
        }
    }
}
