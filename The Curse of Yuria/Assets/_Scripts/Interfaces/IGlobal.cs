using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public interface IGlobal
{
    List<IPlayer> getParty { get; }
    List<IActor> getActors { get; }
    Camera getCamera { get; }

    Queue<IActor> aTBGuageFilledQueue { get; set; }
    Queue<ICommand> commandQueue { get; }

    Dictionary<IItem.Category, Inventory> inventories { get; }

    ShakeData getShakeData { get; }
    AudioSource getAudioSource { get; }

    IInventory getCompletedQuests { get; }
    IInventory getCompletedIds { get; }

    RectTransform getCanvas { get; }
    RectTransform getTitleScreenDisplay { get; }
    RectTransform getPromptDisplay { get; }
    RectTransform getEquipmentDisplay { get; }
    RectTransform getScrollDisplay { get; }
    RectTransform getCommandDisplay { get; }
    RectTransform getOptionsDisplay { get; }
    RectTransform getGameOverDisplay { get; }
    
    Coroutine StartCoroutine(IEnumerator routine);
}
