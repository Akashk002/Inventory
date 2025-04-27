using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] private List<SlotList> slotLists;
   
    public List<Slot> GetSlotLists(SlotType slotType)
    {
        List <Slot> SlotList = slotLists.Find(list => list.slotType == slotType).slotList;

        return SlotList;
    }
}

[System.Serializable]

public class SlotList
{
    public SlotType slotType;
    public List<Slot> slotList;
}