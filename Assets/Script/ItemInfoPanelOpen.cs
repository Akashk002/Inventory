using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfoPanelOpen : MonoBehaviour
{
    [SerializeField] ItemInfoManager ItemBuyPanel, ItemSellPanel ;
    [SerializeField] GameObject ItemDealConfirmationPanel;
    [SerializeField] TMP_Text confirmationText;

    private void OnEnable()
    {
        GameAction.OnSlotSelect = OpenItemInfoPanel;
    }

    private void OnDisable()
    {
        GameAction.OnSlotSelect -= OpenItemInfoPanel;
    }

    void OpenItemInfoPanel(SlotView slotView)
    {
        if (!ItemBuyPanel.gameObject.activeInHierarchy || !ItemSellPanel.gameObject.activeInHierarchy)
        {
            if (slotView.GetSlotType() == SlotType.Shop)
            {
                ItemBuyPanel.gameObject.SetActive(true);
                ItemBuyPanel.OpenInventoryItemInfoPanel(slotView);
            }
            else
            {
                ItemSellPanel.gameObject.SetActive(true);
                ItemSellPanel.OpenInventoryItemInfoPanel(slotView);
            }
           
        }
    }

    public void OpenItemDealConfirmationPanel(SlotView slotView)
    {
        confirmationText.text = $"Do You Want to buy 1 Sword for 900 coins";
        ItemDealConfirmationPanel.SetActive(true);
    }
}
