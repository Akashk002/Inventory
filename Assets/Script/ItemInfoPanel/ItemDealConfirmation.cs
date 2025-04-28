using UnityEngine;
using TMPro;

public class ItemDealConfirmation : MonoBehaviour
{
    [SerializeField] private TMP_Text confirmationText;

    private void OnEnable()
    {
        GameService.Instance.GetEventService().OnBuyItem.AddListener(UpdateBuyConfirmationText);
        GameService.Instance.GetEventService().OnSellItem.AddListener(UpdateSellConfirmationText);
    }

    private void OnDisable()
    {
        GameService.Instance.GetEventService().OnBuyItem.RemoveListener(UpdateBuyConfirmationText);
        GameService.Instance.GetEventService().OnSellItem.RemoveListener(UpdateSellConfirmationText);
    }

    private void UpdateBuyConfirmationText(Slot slotView, int quantity)
    {
        InventoryItem inventoryItem = slotView.GetInventoryItem();
        confirmationText.text = $"Do You Want to Buy {quantity} {inventoryItem.Name} for {inventoryItem.buyingPrice * quantity} coins";
    }

    private void UpdateSellConfirmationText(Slot slotView, int quantity)
    {
        InventoryItem inventoryItem = slotView.GetInventoryItem();
        confirmationText.text = $"Do You Want to Sell {quantity} {inventoryItem.Name} for {inventoryItem.sellingPrice * quantity} coins";
    }
}
