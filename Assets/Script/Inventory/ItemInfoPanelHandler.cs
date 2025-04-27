using TMPro;
using UnityEngine;

public class ItemInfoPanelHandler : MonoBehaviour
{
    [SerializeField] private ItemInfoHandler ItemBuyPanel, ItemSellPanel ;
    [SerializeField] private GameObject ItemDealConfirmationPanel;
    [SerializeField] private TMP_Text confirmationText;

    private void OnEnable()
    {
        EventService.Instance.OnSlotSelect.AddListener(OpenItemInfoPanel);
    }

    private void OnDisable()
    {
        EventService.Instance.OnSlotSelect.RemoveListener(OpenItemInfoPanel);
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

            AudioManager.Instance.Play(SoundType.SelectSlot);
        }
    }

}
