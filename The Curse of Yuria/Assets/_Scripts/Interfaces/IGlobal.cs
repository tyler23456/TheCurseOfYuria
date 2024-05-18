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

    Transform getAllieRoot { get; }
    List<IActor> getActors { get; }
    Camera getCamera { get; }

    Queue<IActor> aTBGuageFilledQueue { get; }
    Queue<Command> pendingCommands { get; }
    List<Subcommand> successfulSubcommands { get; }

    Dictionary<IItem.Category, Inventory> inventories { get; }
    RectTransform getCanvas { get; }

    ShakeData getShakeData { get; }
    AudioSource getAudioSource { get; }

    IInventory getCompletedQuests { get; }
    IInventory getCompletedIds { get; }

    int getPartyMemberCount { get; }
    IPartyMember GetPartyMember(int i);
    void ToggleDisplay(Display display);
    void ClearAllInventories();
    void DestroyAllPartyMembers();
    void AddDamagePopup(string damage, Vector3 position);
    void AddRecoveryPopup(string recovery, Vector3 position);
    public void ClearAllPopups();

    Coroutine StartCoroutine(IEnumerator routine);
}
