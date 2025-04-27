using UnityEngine;
using TMPro;

public class ItemDealConfirmation : MonoBehaviour
{
    [SerializeField] private TMP_Text confirmationText;

    private void OnEnable()
    {
        EventService.Instance.OnBuyItem.AddListener(UpdateBuyConfirmationText);
        EventService.Instance.OnSellItem.AddListener(UpdateSellConfirmationText);
    }

    private void OnDisable()
    {
        EventService.Instance.OnBuyItem.RemoveListener(UpdateBuyConfirmationText);
        EventService.Instance.OnSellItem.RemoveListener(UpdateSellConfirmationText);
    }

    private void UpdateBuyConfirmationText(Slot slotView,int quantity)
    {
        InventoryItem inventoryItem = slotView.GetInventoryItem();
        confirmationText.text = $"Do You Want to Buy {quantity} {inventoryItem.Name} for {inventoryItem.buyingPrice * quantity} coins";
    }
    
    private void UpdateSellConfirmationText(Slot slotView,int quantity)
    {
        InventoryItem inventoryItem = slotView.GetInventoryItem();
        confirmationText.text = $"Do You Want to Sell {quantity} {inventoryItem.Name} for {inventoryItem.sellingPrice * quantity} coins";
    }
}
