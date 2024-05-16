using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine.UI;
using TMPro;

public interface IGlobal
{
    enum Display
    {
        MainMenuDisplay,
        LoadingDisplay,
        PromptDisplay,
        EquipmentDisplay,
        ScrollDisplay,
        CommandDisplay,
        OptionsDisplay,
        GameOverDisplay
    }

    enum GameState { Playing, Paused, Stopped }

    static GameState gameState = GameState.Stopped;

    int sceneIDToLoad { get; set; }
    Vector2 scenePositionToStart { get; set; }
    float sceneEulerAngleZToStart { get; set; }

    GameObject getPartyRoot { get; }
    List<IActor> getActors { get; }
    Camera getCamera { get; }

    Queue<IActor> aTBGuageFilledQueue { get; set; }
    Queue<ICommand> pendingCommands { get; }

    Dictionary<IItem.Category, Inventory> inventories { get; }
    RectTransform getCanvas { get; }

    ShakeData getShakeData { get; }
    AudioSource getAudioSource { get; }

    IInventory getCompletedQuests { get; }
    IInventory getCompletedIds { get; }

    int getPartyMemberCount { get; }
    IPartyMember GetPartyMember(int i);
    void ToggleDisplay(Display display);

    Coroutine StartCoroutine(IEnumerator routine);
}
