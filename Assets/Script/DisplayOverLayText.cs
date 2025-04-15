using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayOverLayText : MonoBehaviour
{
    [SerializeField] TMP_Text OverlayText;
    [SerializeField] string buyFailedByCoin,buyfailedByInventoryWeight;

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

    void UpdateTextOnBuyItem(InventoryItem inventoryItem, int quantity)
    {
        OverlayText.enabled = true;
        OverlayText.text = $"You bought {quantity} {inventoryItem.name}";
        OverlayText.color = Color.green;
        Invoke(nameof(DisableOverLayText), 2f);
    }
    void UpdateTextOnSellItem(InventoryItem inventoryItem, int quantity)
    {
        OverlayText.enabled = true;
        OverlayText.text = $"You gained {inventoryItem.sellingPrice * quantity} coins";
        OverlayText.color = Color.green;
        Invoke(nameof(DisableOverLayText), 2f);
    } 
    
    void UpdateTextOnBuyFailed(BuyFailedType buyFailedType)
    {
        string failedText = (buyFailedType == BuyFailedType.Coin) ? buyFailedByCoin : buyfailedByInventoryWeight;
        AudioManager.Instance.Play(SoundType.BuyFailed);
        OverlayText.enabled = true;
        OverlayText.text = failedText;
        OverlayText.color = Color.red;
        Invoke(nameof(DisableOverLayText), 3f);
    }

    void DisableOverLayText()
    {
        OverlayText.enabled = false;
    }
}
