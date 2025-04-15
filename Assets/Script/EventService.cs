using System;
public class EventService 
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }

    public EventController<Slot> OnSlotSelect { get; private set; }

    public EventController<Slot,int> OnBuyItem { get; private set; }
    public EventController<Slot,int> OnSellItem { get; private set; }
    public EventController<BuyFailedType> OnBuyFailed { get; private set; }
    public EventController<InventoryItem, int> OnConfirmBuyItem { get; private set; }
    public EventController<InventoryItem, int> OnConfirmSellItem { get; private set; }

    public EventService()
    {
        OnSlotSelect = new EventController<Slot>();
        OnBuyItem = new EventController<Slot,int>();
        OnSellItem = new EventController<Slot,int>();
        OnBuyFailed = new EventController<BuyFailedType>();
        OnConfirmBuyItem = new EventController<InventoryItem,int>();
        OnConfirmSellItem = new EventController<InventoryItem, int>();

    }
}
public class EventController<T,K>
{
    public event Action<T,K> baseEvent;
    public void InvokeEvent(T type,K type2) => baseEvent?.Invoke(type,type2);
    public void AddListener(Action<T,K> listener) => baseEvent += listener;
    public void RemoveListener(Action<T,K> listener) => baseEvent -= listener;
}

public class EventController<T>
{
    public event Action<T> baseEvent;
    public void InvokeEvent(T type) => baseEvent?.Invoke(type);
    public void AddListener(Action<T> listener) => baseEvent += listener;
    public void RemoveListener(Action<T> listener) => baseEvent -= listener;
}


