using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobal
{
    List<IPlayer> getParty { get; }
    List<IActor> getActors { get; }
    Camera getCamera { get; }

    Queue<IPlayer.Names> aTBGuageFilledQueue { get; set; }
    Queue<(IActor user, string command, IActor target)> commandQueue { get; set; }

    IInventory getHelmets { get; }
    IInventory getEarrings { get; }
    IInventory getGlasses { get; }
    IInventory getMasks { get; }
    IInventory getMeleeWeapons1H { get; }
    IInventory getMeleeWeapons2H { get;}
    IInventory getCapes { get; }
    IInventory getArmor { get; }
    IInventory getShields { get; }
    IInventory getBows { get; }
    IInventory getSupplies { get; }
    IInventory getQuestItems { get; }
    IInventory getCompletedQuests { get; }
    IInventory getCompletedIds { get; }

    IInventory GetInventoryOf(string itemName);
}
