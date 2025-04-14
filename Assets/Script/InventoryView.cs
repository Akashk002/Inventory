using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] InventoryItemList inventoryItemList;
    [SerializeField] GridLayoutGroup inventory;

    // Start is called before the first frame update
    void Start()
    {
        SetUpShop();

    }

    void SetUpShop()
    {
        List<InventoryItem> materialList = inventoryItemList.MaterialsList;
        List<InventoryItem> WeaponList = inventoryItemList.WeaponsList;
        List<InventoryItem> consumableList = inventoryItemList.ConsumablesList;
        List<InventoryItem> treasureList = inventoryItemList.TreasureList;

        for (int i = 0; i < materialList.Count; i++)
        {
             inventory.transform.GetChild(i).GetComponent<SlotView>().SetSlotInfo(materialList[i], SlotType.Inventory);
        } 
        for (int i = 12; i < 24; i++)
        {
             inventory.transform.GetChild(i).GetComponent<SlotView>().SetSlotInfo(WeaponList[i - 12], SlotType.Inventory);
        } 
        for (int i = 24; i < 36; i++)
        {
             inventory.transform.GetChild(i).GetComponent<SlotView>().SetSlotInfo(consumableList[i - 24], SlotType.Inventory);
        } 
        for (int i = 36; i < 48; i++)
        {
             inventory.transform.GetChild(i).GetComponent<SlotView>().SetSlotInfo(treasureList[i - 36], SlotType.Inventory);
        }
    }
}
