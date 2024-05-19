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

    Camera getCamera { get; }
    Actors allies { get; }
    Actors enemies { get; }

    Queue<IActor> aTBGuageFilledQueue { get; }
    LinkedList<Command> pendingCommands { get; }
    LinkedList<Command> successfulCommands { get; }

    Dictionary<IItem.Category, Inventory> inventories { get; }
    RectTransform getCanvas { get; }

    ShakeData getShakeData { get; }
    AudioSource getAudioSource { get; }

    IInventory getCompletedQuests { get; }
    IInventory getCompletedIds { get; }

    void ToggleDisplay(Display display);
    void ClearAllInventories();
    void AddDamagePopup(string damage, Vector3 position);
    void AddRecoveryPopup(string recovery, Vector3 position);
    void ClearAllPopups();

    Coroutine StartCoroutine(IEnumerator routine);
    void StopAllCoroutines();
}
