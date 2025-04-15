using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfoHandler : MonoBehaviour
{
    [SerializeField] Slot selectedSlot;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] TMP_Text itemPrice;
    [SerializeField] TMP_Text itemWeight;
    [SerializeField] TMP_Text itemQuantity;
    [SerializeField] TMP_Text itemRarity;
    [SerializeField] TMP_Text setQuantity;
    [SerializeField] TMP_Text totalPrice;
    InventoryItem selectedInventoryItem;
    int currentQuantity = 1;

    private void OnEnable()
    {
        EventService.Instance.OnSlotSelect.AddListener(OpenInventoryItemInfoPanel);
    }
    private void OnDisable()
    {
        EventService.Instance.OnSlotSelect.RemoveListener(OpenInventoryItemInfoPanel);
        if (selectedSlot) selectedSlot.DisableSelectionBox();
    }

    public void OpenInventoryItemInfoPanel(Slot slot)
    {
        if (slot.GetInventoryItem() == null && slot.GetItemQuantity() <= 0) return;
        if (selectedSlot) selectedSlot.DisableSelectionBox();

        selectedSlot = slot;
        selectedInventoryItem = slot.GetInventoryItem();

        if (selectedSlot)
        {
            currentQuantity = 1;
            int price = (selectedSlot.GetSlotType() == SlotType.Shop) ? selectedInventoryItem.buyingPrice : selectedInventoryItem.sellingPrice ;
            itemImage.sprite = selectedInventoryItem.icon;
            itemName.text = selectedInventoryItem.name;
            itemDescription.text = selectedInventoryItem.itemDescription;
            itemPrice.text = price.ToString();
            itemWeight.text = selectedInventoryItem.weight.ToString();
            itemQuantity.text = selectedSlot.GetItemQuantity().ToString();
            itemRarity.text = selectedInventoryItem.rarity.ToString();
            setQuantity.text = currentQuantity.ToString();
            totalPrice.text = price.ToString();
            selectedSlot.EnableSelectionBox();
        }
    }

    public void DecreaseItemQuantity()
    {
        if (selectedInventoryItem && currentQuantity > 1 && selectedSlot.GetItemQuantity() > 0)
        {
            int price = (selectedSlot.GetSlotType() == SlotType.Shop) ? selectedInventoryItem.buyingPrice : selectedInventoryItem.sellingPrice;
            currentQuantity--;
            setQuantity.text = currentQuantity.ToString();
            totalPrice.text = (selectedInventoryItem.buyingPrice * currentQuantity).ToString();
            AudioManager.Instance.PlayClickSound();
        }
    }

    public void IncreaseItemQuantity()
    {   
        if (selectedInventoryItem && currentQuantity < selectedSlot.GetItemQuantity() && selectedSlot.GetItemQuantity() > 0)
        {
            int price = (selectedSlot.GetSlotType() == SlotType.Shop) ? selectedInventoryItem.buyingPrice : selectedInventoryItem.sellingPrice;
            currentQuantity++;
            setQuantity.text = currentQuantity.ToString();
            totalPrice.text = (price * currentQuantity).ToString();
            AudioManager.Instance.PlayClickSound();
        }
    }

    public void OnBuyItem()
    {
        EventService.Instance.OnBuyItem.InvokeEvent(selectedSlot, currentQuantity);
        AudioManager.Instance.PlayClickSound();
    }

    public void OnSellItem()
    {
        EventService.Instance.OnSellItem.InvokeEvent(selectedSlot, currentQuantity);
        AudioManager.Instance.PlayClickSound();
    }  
}
