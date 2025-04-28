using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup materialPanel;
    [SerializeField] private List<SlotList> slotLists;
    [SerializeField] private ShopController shopController;

    public GridLayoutGroup GetmaterialPanel()
    {
        return materialPanel;
    }   
    
    public List<SlotList> GetSlotLists()
    {
        return slotLists;
    }  
    
    public void SetShopController( ShopController shopController)
    {
       this.shopController = shopController;
    }

    private void Start()
    {
        shopController.OnEnable();
    }

    private void OnDisable()
    {
        shopController.OnDisable();
    }
}
