using TMPro;
using UnityEngine;

public class ItemInfoPanelHandler : MonoBehaviour
{
    [SerializeField] private ItemInfoHandler ItemBuyPanel, ItemSellPanel;
    [SerializeField] private GameObject ItemDealConfirmationPanel;
    [SerializeField] private TMP_Text confirmationText;

    public void SubscribeEvent()
    {
        GameService.Instance.GetEventService().OnSlotSelect.AddListener(OpenItemInfoPanel);
    }

    private void OnDisable()
    {
        GameService.Instance.GetEventService().OnSlotSelect.RemoveListener(OpenItemInfoPanel);
    }

    private void OpenItemInfoPanel(Slot slot)
    {
        if (slot.GetInventoryItem() == null && slot.GetItemQuantity() <= 0) return;
        if (!ItemBuyPanel.gameObject.activeInHierarchy || !ItemSellPanel.gameObject.activeInHierarchy)
        {
            if (slot.GetSlotType() != SlotType.Inventory)
            {
                ItemBuyPanel.gameObject.SetActive(true);
                ItemBuyPanel.OpenInventoryItemInfoPanel(slot);
            }
            else
            {
                ItemSellPanel.gameObject.SetActive(true);
                ItemSellPanel.OpenInventoryItemInfoPanel(slot);
            }

            GameService.Instance.GetAudioService().Play(SoundType.SelectSlot);
        }
    }

}
