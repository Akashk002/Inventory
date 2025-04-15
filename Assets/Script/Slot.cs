using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] SlotType slotType;
    [SerializeField] Image itemImage;
    [SerializeField] Image selectioxBox;
    [SerializeField] TMP_Text itemQuantityText;
    [SerializeField] InventoryItem inventoryItem;
     int itemQuantity;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (inventoryItem && itemQuantity > 0)
        {
            itemImage.enabled = true;
            itemQuantityText.enabled = true;
            itemImage.sprite = inventoryItem.icon;
            itemQuantityText.text = itemQuantity.ToString();
        }
        else
        {
            ResetSlot();
        }
    }

    public void AddNewItemInSlot(InventoryItem Inventoryitem,int quantity)
    {
        inventoryItem = Inventoryitem;
        itemQuantity = quantity;
        UpdateSlot();
    } 
    
    public void AddSameItemInSlot(int quantity)
    {
        itemQuantity += quantity;
        UpdateSlot();
    }
    
    public int GetItemQuantity()
    {
        return itemQuantity;
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
        EventService.Instance.OnSlotSelect.InvokeEvent(this);
    }

    public SlotType GetSlotType()
    {
        return slotType;
    }

    public InventoryItem GetInventoryItem()
    {
        return inventoryItem;
    }

    void ResetSlot()
    {
        itemQuantity = 0;
        itemImage.sprite = null;
        itemQuantityText.text = "";
        itemImage.enabled = false;
        itemQuantityText.enabled = false;
        inventoryItem = null;
    }

    public void DecreaseItemQuantity(int quantity)
    {
        if (inventoryItem != null)
        {
            itemQuantity -= quantity;
        }
        UpdateSlot();
    }
}