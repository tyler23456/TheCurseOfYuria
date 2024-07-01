using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchAllieDisplay : DisplayBase
{
    public static DisplayBase Instance { get; protected set; }

    [SerializeField] Transform allies;

    [SerializeField] RectTransform display;
    [SerializeField] RectTransform grid;
    [SerializeField] Button buttonPrefab;
    [SerializeField] Camera userCamera;
    
    Inventory inventory = new Inventory();
    InventoryUI inventoryUI = new InventoryUI();

    bool switching = true;
    float gravityScale = 8f;
    Vector2 selectedDestination = Vector2.zero;
    Vector2 unselectedDestination = Vector2.zero;
    Vector2 distance = new Vector2(-10f, 0f);
    IAllie previousAllie;
    IAllie nextAllie;
    int unselectedIndex = 0;

    float accumulator = 0f;
    float percentageComplete = 0f;
    const float Duration = 1.5f;

    IEnabler cameraFollowEnabler;

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        display.gameObject.SetActive(true);

        inventory.Clear();

        for (int i = 3; i < allies.childCount; i++)
            inventory.Add(allies.GetChild(i).name);

        inventoryUI.showSprite = false;
        inventoryUI.showName = true;
        inventoryUI.showCount = false;
        inventoryUI.grid = grid;
        inventoryUI.buttonPrefab = buttonPrefab;
        inventoryUI.inventory = inventory;
        inventoryUI.OnClick = OnClick;
        inventoryUI.onPointerEnter = OnPointerEnter;
        inventoryUI.onPointerExit = OnPointerExit;
        inventoryUI.Display();

        cameraFollowEnabler = userCamera.GetComponent<IEnabler>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    void OnClick(string allieName)
    {
        cameraFollowEnabler.enabled = false;

        previousAllie = allies.GetChild(0).GetComponent<IAllie>();

        Transform nextAllieTransform = allies.Find(allieName);

        if (nextAllieTransform == null)
            return;

        nextAllie = nextAllieTransform.GetComponent<IAllie>();
       
        if (nextAllie == null)
            return;

        display.gameObject.SetActive(false);
        GameStateManager.Instance.Stop();

        gravityScale = previousAllie.rigidbody2D.gravityScale;

        //previous allie
        selectedDestination = previousAllie.obj.transform.position;
        previousAllie.getCollider2D.enabled = false;
        previousAllie.obj.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        previousAllie.animator.SetInteger("State", 2);
        previousAllie.rigidbody2D.gravityScale = 0f;
        previousAllie.getFadeAnimator.OnCoroutineUpdate = (actor) => OnPreviousActorUpdate(null);
        previousAllie.getFadeAnimator.OnCoroutineEnd = (actor) => OnPreviousActorEnd(null);
        previousAllie.getFadeAnimator.Start(1f, 0f, Duration);

        accumulator = 0f;

        //next allie
        unselectedDestination = previousAllie.rigidbody2D.position + distance;
        nextAllie.obj.SetActive(true);
        nextAllie.getCollider2D.enabled = false;
        nextAllie.obj.transform.position = unselectedDestination;
        nextAllie.obj.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        nextAllie.animator.SetInteger("State", 2);
        nextAllie.rigidbody2D.gravityScale = 0f;
        nextAllie.getFadeAnimator.OnCoroutineUpdate = (actor) => OnNextActorUpdate(null);
        nextAllie.getFadeAnimator.OnCoroutineEnd = (actor) => OnNextActorEnd(null);
        nextAllie.getFadeAnimator.Start(0f, 1f, Duration);


    }

    void OnPreviousActorUpdate(IActor actor)
    {
        accumulator += Time.unscaledDeltaTime;
        percentageComplete = accumulator / Duration;
        previousAllie.obj.transform.position = Vector3.Lerp(selectedDestination, unselectedDestination, percentageComplete);
    }

    void OnNextActorUpdate(IActor actor)
    {
        nextAllie.obj.transform.position = Vector3.Lerp(unselectedDestination, selectedDestination, percentageComplete);
    }

    void OnPreviousActorEnd(IActor actor)
    {
    }

    void OnNextActorEnd(IActor actor)
    {
        nextAllie.rigidbody2D.gravityScale = gravityScale;
        nextAllie.getCollider2D.enabled = true;
        nextAllie.getATBGuage.RaisePriority();

        unselectedIndex = nextAllie.obj.transform.GetSiblingIndex();
        nextAllie.obj.transform.SetSiblingIndex(0);
        previousAllie.obj.transform.SetSiblingIndex(unselectedIndex);
        
        cameraFollowEnabler.enabled = true;
        gameObject.SetActive(false);
        nextAllie.getATBGuage.Reset();
        //call update for other methods
        previousAllie.getATBGuage.LowerPriority();
    }

    void OnPointerEnter(string itemName)
    {

    }

    void OnPointerExit(string itemName)
    {

    }
}
