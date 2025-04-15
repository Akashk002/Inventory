using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfoPanelHandler : MonoBehaviour
{
    [SerializeField] ItemInfoHandler ItemBuyPanel, ItemSellPanel ;
    [SerializeField] GameObject ItemDealConfirmationPanel;
    [SerializeField] TMP_Text confirmationText;

    private void OnEnable()
    {
        EventService.Instance.OnSlotSelect.AddListener(OpenItemInfoPanel);
    }

    private void OnDisable()
    {
        EventService.Instance.OnSlotSelect.RemoveListener(OpenItemInfoPanel);
    }

    void OpenItemInfoPanel(Slot slot)
    {
        if (slot.GetInventoryItem() == null && slot.GetItemQuantity() <= 0) return;
        if (!ItemBuyPanel.gameObject.activeInHierarchy || !ItemSellPanel.gameObject.activeInHierarchy)
        {
            if (slot.GetSlotType() == SlotType.Shop)
            {
                ItemBuyPanel.gameObject.SetActive(true);
                ItemBuyPanel.OpenInventoryItemInfoPanel(slot);
            }
            else
            {
                ItemSellPanel.gameObject.SetActive(true);
                ItemSellPanel.OpenInventoryItemInfoPanel(slot);
            }

            AudioManager.Instance.Play(SoundType.SelectSlot);
        }
    }

}
