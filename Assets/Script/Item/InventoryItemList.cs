using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item List", menuName = "Inventory/Create Inventory Item List")]
public class InventoryItemList : ScriptableObject
{
    public List<InventoryItemTypeData> InventoryItemScriptableList;
}

[System.Serializable]
public class InventoryItemTypeData
{
    public ItemType itemType;
    public List<InventoryItem> itemScriptableList;
}