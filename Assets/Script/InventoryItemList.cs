using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item List", menuName = "Inventory/Create Inventory Item List")]
public class InventoryItemList : ScriptableObject
{
   public List<InventoryItem> MaterialsList;
   public List<InventoryItem> WeaponsList;
   public List<InventoryItem> ConsumablesList;
   public List<InventoryItem> TreasureList;
}

