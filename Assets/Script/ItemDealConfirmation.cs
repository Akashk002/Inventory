using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDealConfirmation : MonoBehaviour
{
    [SerializeField] TMP_Text confirmationText;

    private void OnEnable()
    {
        EventService.Instance.OnBuyItem.AddListener(UpdateBuyConfirmationText);
        EventService.Instance.OnSellItem.AddListener(UpdateSellConfirmationText);
    }

    void OnDisable()
    {
        EventService.Instance.OnBuyItem.RemoveListener(UpdateBuyConfirmationText);
        EventService.Instance.OnSellItem.RemoveListener(UpdateSellConfirmationText);
    }

    void UpdateBuyConfirmationText(Slot slotView,int quantity)
    {
        InventoryItem inventoryItem = slotView.GetInventoryItem();
        confirmationText.text = $"Do You Want to Buy {quantity} {inventoryItem.name} for {inventoryItem.buyingPrice * quantity} coins";
    }
    
    void UpdateSellConfirmationText(Slot slotView,int quantity)
    {
        InventoryItem inventoryItem = slotView.GetInventoryItem();
        confirmationText.text = $"Do You Want to Sell {quantity} {inventoryItem.name} for {inventoryItem.sellingPrice * quantity} coins";
    }
}
