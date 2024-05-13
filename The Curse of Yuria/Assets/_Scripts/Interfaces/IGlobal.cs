using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine.UI;
using TMPro;

public interface IGlobal
{
    GameObject getPartyRoot { get; }
    List<IActor> getActors { get; }
    Camera getCamera { get; }

    Queue<IActor> aTBGuageFilledQueue { get; set; }
    Queue<ICommand> pendingCommands { get; }

    Dictionary<IItem.Category, Inventory> inventories { get; }

    ShakeData getShakeData { get; }
    AudioSource getAudioSource { get; }

    IInventory getCompletedQuests { get; }
    IInventory getCompletedIds { get; }

    RectTransform getCanvas { get; }
    RectTransform getTitleScreenDisplay { get; }
    RectTransform getPromptDisplay { get; }
    Image getPromptDisplayImage { get; }
    TMP_Text getPromptDisplayText { get; }
    RectTransform getEquipmentDisplay { get; }
    RectTransform getScrollDisplay { get; }
    RectTransform getCommandDisplay { get; }
    RectTransform getOptionsDisplay { get; }
    RectTransform getGameOverDisplay { get; }

    int getPartyMemberCount { get; }
    IPartyMember GetPartyMember(int i);


    Coroutine StartCoroutine(IEnumerator routine);
}
