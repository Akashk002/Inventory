using UnityEngine;
using TMPro;

public class DisplayOverlayTextHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text OverlayText;
    [SerializeField] private string buyFailedByCoin;
    [SerializeField] private string buyfailedByInventoryWeight;

    public void SubscribeEvent()
    {
        GameService.Instance.GetEventService().OnConfirmBuyItem.AddListener(UpdateTextOnBuyItem);
        GameService.Instance.GetEventService().OnConfirmSellItem.AddListener(UpdateTextOnSellItem);
        GameService.Instance.GetEventService().OnBuyFailed.AddListener(UpdateTextOnBuyFailed);
    }
    private void OnDisable()
    {
        GameService.Instance.GetEventService().OnConfirmBuyItem.RemoveListener(UpdateTextOnBuyItem);
        GameService.Instance.GetEventService().OnConfirmSellItem.RemoveListener(UpdateTextOnSellItem);
        GameService.Instance.GetEventService().OnBuyFailed.RemoveListener(UpdateTextOnBuyFailed);
    }

    private void UpdateTextOnBuyItem(InventoryItem inventoryItem, int quantity)
    {
        OverlayText.enabled = true;
        OverlayText.text = $"You bought {quantity} {inventoryItem.Name}";
        OverlayText.color = Color.green;
        Invoke(nameof(DisableOverLayText), 2f);
    }
    private void UpdateTextOnSellItem(InventoryItem inventoryItem, int quantity)
    {
        OverlayText.enabled = true;
        OverlayText.text = $"You gained {inventoryItem.sellingPrice * quantity} coins";
        OverlayText.color = Color.green;
        Invoke(nameof(DisableOverLayText), 2f);
    }

    private void UpdateTextOnBuyFailed(BuyFailedType buyFailedType)
    {
        string failedText = (buyFailedType == BuyFailedType.Coin) ? buyFailedByCoin : buyfailedByInventoryWeight;
        GameService.Instance.GetAudioService().Play(SoundType.BuyFailed);
        OverlayText.enabled = true;
        OverlayText.text = failedText;
        OverlayText.color = Color.red;
        Invoke(nameof(DisableOverLayText), 3f);
    }

    private void DisableOverLayText()
    {
        OverlayText.enabled = false;
    }
}

[System.Serializable]
public enum BuyFailedType
{
    Coin,
    InventoryWeight
}