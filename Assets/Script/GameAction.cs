using System;

public static class GameAction 
{
    public static Action<SlotView> OnSlotSelect;
    public static Action<SlotView,int> OnBuyOrSellItem;
    public static Action<SlotView> OnConfirmBuyItem;
    public static Action<SlotView> OnConfirmSellItem;
}
