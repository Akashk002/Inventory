using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Inventory/Create New Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public ItemName Name;
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
    Materials = 0,
    Weapons = 1,
    Consumables = 2,
    Treasure = 3
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

public enum ItemName
{
    Apple,
    Cabbage,
    Capcicum,
    Fish,
    Grapes,
    GreenHerbs,
    GreenPotion,
    Meat,
    Mushroom,
    RedPotion,
    Stawberry,
    YellowPotion,
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
    WoodLogs,
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
    TreasureBox,
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
