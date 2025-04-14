using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotView : MonoBehaviour
{
    [SerializeField] SlotType slotType;
    [SerializeField] Image itemImage;
    [SerializeField] Image selectioxBox;
    [SerializeField] TMP_Text itemQuantityText;
    [SerializeField] InventoryItem inventoryItem;
    [SerializeField] int itemQuantity;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (inventoryItem)
        {
            itemQuantity = inventoryItem.quantity;
            
            if(itemQuantity > 0)
            {
                itemImage.enabled = true;
                itemQuantityText.enabled = true;
                itemImage.sprite = inventoryItem.icon;
                itemQuantityText.text = inventoryItem.quantity.ToString();
            }
            else
            {
                itemImage.enabled = false; 
                itemQuantityText.enabled = false;
            }
        }
        else
        {
            //itemImage.enabled = false;
            itemQuantityText.enabled = false;
        }
    }

    public void SetSlotInfo(InventoryItem Inventoryitem ,SlotType slotType)
    {
        inventoryItem = Inventoryitem;
        this.slotType = slotType;
        UpdateSlot();
    }
    
    public void SetItemQuantity(int quanTity)
    {
        itemQuantity = quanTity;
    }

    public void DisableSelectionBox()
    {
        selectioxBox.enabled = false;
    }
    public void EnableSelectionBox()
    {
        selectioxBox.enabled = true;
    }

    public void OnSelectSlot()
    {
        GameAction.OnSlotSelect?.Invoke(this);
    }

    public SlotType GetSlotType()
    {
        return slotType;
    }

    public InventoryItem GetInventoryItem()
    {
        return inventoryItem;
    }
}