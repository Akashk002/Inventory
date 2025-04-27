using UnityEngine;
using TMPro;

public class DisplayOverlayText : MonoBehaviour
{
    [SerializeField] private TMP_Text OverlayText;
    [SerializeField] private string buyFailedByCoin;
    [SerializeField] private string buyfailedByInventoryWeight;

    private void OnEnable()
    {
        EventService.Instance.OnConfirmBuyItem.AddListener(UpdateTextOnBuyItem);
        EventService.Instance.OnConfirmSellItem.AddListener(UpdateTextOnSellItem);
        EventService.Instance.OnBuyFailed.AddListener(UpdateTextOnBuyFailed);
    }
    private void OnDisable()
    {
        EventService.Instance.OnConfirmBuyItem.RemoveListener(UpdateTextOnBuyItem);
        EventService.Instance.OnConfirmSellItem.RemoveListener(UpdateTextOnSellItem);
        EventService.Instance.OnBuyFailed.RemoveListener(UpdateTextOnBuyFailed);
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
        AudioManager.Instance.Play(SoundType.BuyFailed);
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