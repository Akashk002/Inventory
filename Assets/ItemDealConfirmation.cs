using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDealConfirmation : MonoBehaviour
{
    [SerializeField] TMP_Text confirmationText;
    private void OnEnable()
    {
        GameAction.OnBuyOrSellItem += UpdateConfirmationText;
    }

    void OnDisable()
    {
        GameAction.OnBuyOrSellItem -= UpdateConfirmationText;
    }

    void UpdateConfirmationText(SlotView slotView,int quantity)
    {
        InventoryItem inventoryItem = slotView.GetInventoryItem();
        string text = slotView.GetSlotType() == SlotType.Shop ? "Buy" : "Sell";
        int cost = slotView.GetSlotType() == SlotType.Shop ? inventoryItem.buyingPrice : inventoryItem.sellingPrice;
        string itemName = inventoryItem.name;

        confirmationText.text = $"Do You Want to {text} {quantity} {itemName} for {cost * quantity} coins";
    }
}
