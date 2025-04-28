using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryService : MonoBehaviour
{
    private InventoryController inventoryController;

    // Start is called before the first frame update
    public InventoryService(InventoryView inventoryView, InventoryItemList inventoryItemList)
    {
        inventoryController = new InventoryController(inventoryView, inventoryItemList);
    }
    public InventoryController GetInventoryController()
    {
        return inventoryController;
    }
}
