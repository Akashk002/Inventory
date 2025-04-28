using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text weightText;

    [SerializeField] private Transform parentTranform;
    [SerializeField] private int totalSlot = 48;
    [SerializeField] private int maxWeight = 1000;
    [SerializeField] private int numOfTypesOfItem = 4;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private ItemInfoPanelHandler infoPanelHandler;
    [SerializeField] private DisplayOverlayTextHandler displayOverlayTextHandler;

    [SerializeField] private InventoryItemList inventoryItemList;
    [SerializeField] private AudioScriptableObject audioScriptableObject;

    [SerializeField] private ShopView shopView;
    [SerializeField] private InventoryView inventoryView;

    private ShopService shopService;
    private InventoryService inventoryService;
    private AudioService audioService;
    private EventService eventService;

    private int coin;
    private int currentWeight;


    private void Start()
    {
        eventService = new EventService();
        audioService = new AudioService(audioSource, audioScriptableObject);
        shopService = new ShopService(shopView, inventoryItemList);
        inventoryService = new InventoryService(inventoryView, inventoryItemList);
        infoPanelHandler.SubscribeEvent();
        displayOverlayTextHandler.SubscribeEvent();
        UpdateWeight(currentWeight);
    }

    public int GetCoin()
    {
        return coin;
    }
    public void AddCoin(int val)
    {
        coin += val;
        UpdateCoinText(coin);
    }
    public void SpentCoin(int itemCost)
    {
        if (coin == 0 || itemCost > coin)
        {
            return;
        }

        coin -= itemCost;
        UpdateCoinText(coin);
    }


    private void UpdateCoinText(int val)
    {
        coinText.text = val.ToString();
    }

    public void UpdateWeight(int weight)
    {
        currentWeight += weight;
        weightText.text = $"{currentWeight}/{maxWeight}";
    }

    public int GetRemainingWeight()
    {
        return maxWeight - currentWeight;
    }

    public void AddResourse()
    {
        ItemType randomItemType = (ItemType)Random.Range(0, numOfTypesOfItem);

        List<InventoryItem> invenitoryItemList = inventoryItemList.InventoryItemScriptableList.Find(list => list.itemType == randomItemType).itemScriptableList;

        int randomItem = Random.Range(0, invenitoryItemList.Count);
        InventoryItem inventoryItem = invenitoryItemList[randomItem];

        if (inventoryItem.weight > GetRemainingWeight())
        {
            eventService.OnBuyFailed.InvokeEvent(BuyFailedType.InventoryWeight);
            return;
        }

        audioService.Play(SoundType.AddResource);

        eventService.OnAddResourceInInventory.InvokeEvent(inventoryItem, 1);
    }

    public AudioService GetAudioService()
    {
        return audioService;
    }

    public EventService GetEventService()
    {
        return eventService;
    }

    public Transform GetParentTransform()
    {
        return parentTranform;
    }

    public void ConfirmBuyItemFromShop()
    {
        shopService.GetShopController().BuyConfirm();
    }

    public void ConfirmSellItemFromInventory()
    {
        inventoryService.GetInventoryController().SellConfirm();
    }

}
