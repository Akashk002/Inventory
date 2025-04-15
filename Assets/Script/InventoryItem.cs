using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Inventory/Create New Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public ItemType type;
    public Sprite icon;
    public string itemDescription;
    public int buyingPrice;
    public int sellingPrice;
    public int weight;
    public int quantity;
    public Rarity rarity;
}
[System.Serializable]
public enum ItemType
{
    Materials,
    Weapons,
    Consumables,
    Treasure
}
[System.Serializable]
public enum Rarity
{
    VeryCommon,
    Common,
    Rare,
    Epic,
    Legendary
}

[System.Serializable]
public enum SlotType
{
    Shop,
    Inventory
}

[System.Serializable]
public enum BuyFailedType
{
    Coin,
    InventoryWeight
}