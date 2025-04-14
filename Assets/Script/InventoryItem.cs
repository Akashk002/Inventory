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
public enum WeaponType
{
    Axe,
    ButcherKnife,
    Crusher,
    Hammer,
    Harpoon,
    Katana,
    Knife,
    OldPoleaxe,
    Spear,
    Sword,
    WoodcutterAxe,
    WoodenClub
}
[System.Serializable]
public enum ConsumableType
{
    Apple,
    Cabbage,
    Capsicum,
    Fish,
    Grapes,
    GreenHerbs,
    GreenPotion,
    Meat,
    Mushroom,
    RedPotion,
    Stawberry,
    YellowPotion

}
[System.Serializable]
public enum MaterialType
{
    Bone,
    Cloth,
    Gold,
    Iron,
    LeatherPouch,
    Paper,
    Silver,
    Skull,
    Stone,
    WoodBirch,
    WoodenStick,
    WoodLogs
}
[System.Serializable]
public enum TreasureType
{
    BlueDiamondRing,
    Crown,
    GoldCoins,
    GoldenRing,
    GreenStoneRing,
    Necklace,
    RedDiamond,
    RedDiamondRing,
    SilverCoins,
    SilverKey,
    RareStone,
    TreasureBox
}

[System.Serializable]
public enum SlotType
{
    Shop,
    Inventory
}