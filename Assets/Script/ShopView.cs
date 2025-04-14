using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] InventoryItemList inventoryItemList;
    [SerializeField] GridLayoutGroup materials, weapon, consumable,treasure;

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
            materials.transform.GetChild(i).GetComponent<SlotView>().SetSlotInfo(materialList[i], SlotType.Shop);
        } 
        
        for (int i = 0; i < WeaponList.Count; i++)
        {
            weapon.transform.GetChild(i).GetComponent<SlotView>().SetSlotInfo(WeaponList[i], SlotType.Shop);
        } 
        
        for (int i = 0; i < consumableList.Count; i++)
        {
            consumable.transform.GetChild(i).GetComponent<SlotView>().SetSlotInfo(consumableList[i], SlotType.Shop);
        } 
        
        for (int i = 0; i < treasureList.Count; i++)
        {
            treasure.transform.GetChild(i).GetComponent<SlotView>().SetSlotInfo(treasureList[i], SlotType.Shop);
        }

        materials.gameObject.SetActive(true);
    }
}
