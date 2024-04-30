using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobal
{
    List<IPlayer> getParty { get; }
    List<IActor> getActors { get; }
    Camera getCamera { get; }

    Queue<IPlayer.Names> aTBGuageFilledQueue { get; set; }
    Queue<ICommand> commandQueue { get; }

    Dictionary<IItem.Category, Inventory> inventories { get; }

    IInventory getCompletedQuests { get; }
    IInventory getCompletedIds { get; }

    Coroutine StartCoroutine(IEnumerator routine);
}
