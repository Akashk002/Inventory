using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfoManager : MonoBehaviour
{
    [SerializeField] SlotView selectedSlot;
    InventoryItem selectedInventoryItem;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] TMP_Text itemBuyPrice;
    [SerializeField] TMP_Text itemSellPrice;
    [SerializeField] TMP_Text itemWeight;
    [SerializeField] TMP_Text itemQuantity;
    [SerializeField] TMP_Text itemRarity;
    [SerializeField] TMP_Text setQuantity;
    [SerializeField] TMP_Text totalPrice;
    int currentQuantity = 1;

    private void OnEnable()
    {
        GameAction.OnSlotSelect += OpenInventoryItemInfoPanel;
    }
    private void OnDisable()
    {
        GameAction.OnSlotSelect -= OpenInventoryItemInfoPanel;
        if (selectedSlot) selectedSlot.DisableSelectionBox();
    }

    public void OpenInventoryItemInfoPanel(SlotView slotView)
    {
        if (selectedSlot) selectedSlot.DisableSelectionBox();

        selectedSlot = slotView;
        selectedInventoryItem = slotView.GetInventoryItem();

        if (selectedSlot)
        {
            int price = (selectedSlot.GetSlotType() == SlotType.Shop) ? selectedInventoryItem.buyingPrice : selectedInventoryItem.sellingPrice ;
            itemImage.sprite = selectedInventoryItem.icon;
            itemName.text = selectedInventoryItem.name;
            itemDescription.text = selectedInventoryItem.itemDescription;
            itemBuyPrice.text = price.ToString();
            itemSellPrice.text = price.ToString();
            itemWeight.text = selectedInventoryItem.weight.ToString();
            itemQuantity.text = selectedInventoryItem.quantity.ToString();
            itemRarity.text = selectedInventoryItem.rarity.ToString();
            currentQuantity = 1;
            setQuantity.text = currentQuantity.ToString();
            totalPrice.text = price.ToString();
            selectedSlot.EnableSelectionBox();
        }
    }

    public void DecreaseQuantity()
    {
        if (selectedInventoryItem && currentQuantity > 1 && selectedInventoryItem.quantity > 0)
        {
            int price = (selectedSlot.GetSlotType() == SlotType.Shop) ? selectedInventoryItem.buyingPrice : selectedInventoryItem.sellingPrice;
            currentQuantity--;
            setQuantity.text = currentQuantity.ToString();
            totalPrice.text = (selectedInventoryItem.buyingPrice * currentQuantity).ToString();
        }
    }

    public void IncreaseQuantity()
    {   
        if (selectedInventoryItem && currentQuantity < selectedInventoryItem.quantity && selectedInventoryItem.quantity > 0)
        {
            int price = (selectedSlot.GetSlotType() == SlotType.Shop) ? selectedInventoryItem.buyingPrice : selectedInventoryItem.sellingPrice;
            currentQuantity++;
            setQuantity.text = currentQuantity.ToString();
            totalPrice.text = (price * currentQuantity).ToString();
        }
    }

    public void OnBuyOrSellItem()
    {
        GameAction.OnBuyOrSellItem?.Invoke(selectedSlot, currentQuantity);
    }
}
