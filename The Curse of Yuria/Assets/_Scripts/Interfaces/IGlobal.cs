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

    AudioSource getAudioSource { get; }

    IInventory getCompletedQuests { get; }
    IInventory getCompletedIds { get; }

    RectTransform getTitleScreenDisplay { get; }
    RectTransform getPromptDisplay { get; }
    RectTransform getEquipmentDisplay { get; }
    RectTransform getCommandDisplay { get; }
    RectTransform getOptionsDisplay { get; }
    RectTransform getGameOverDisplay { get; }
    
    Coroutine StartCoroutine(IEnumerator routine);
}
