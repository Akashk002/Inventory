using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private List<Slot> inventorySlotLists;
    [SerializeField] private InventoryController inventoryController;
    public List<Slot> GetSlotLists()
    {
        return inventorySlotLists;
    }

    public void SetInventoryController(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
    }

    private void Start()
    {
        inventoryController.OnEnable();
    }

    private void OnDisable()
    {
        inventoryController.OnDisable();
    }
}
