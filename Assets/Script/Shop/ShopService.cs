using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopService
{
    private ShopController shopController;

    // Start is called before the first frame update
    public ShopService(ShopView shopView ,InventoryItemList inventoryItemList)
    {
        shopController = new ShopController(shopView,inventoryItemList);
    }

    public ShopController GetShopController()
    {
        return shopController;
    }

}
