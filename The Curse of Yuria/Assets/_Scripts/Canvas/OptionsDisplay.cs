using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsDisplay : DisplayBase
{
    public static DisplayBase Instance { get; protected set; }

    [SerializeField] Button graphicsTab;
    [SerializeField] Button SettingsTab;
    [SerializeField] Button controlsTab;
    [SerializeField] Button saveTab;
    [SerializeField] Button quitTab;
    [SerializeField] Button exitButton;

    [SerializeField] RectTransform graphicsDisplay;
    [SerializeField] RectTransform settingsDisplay;
    [SerializeField] RectTransform controlsDisplay;
    [SerializeField] RectTransform saveDisplay;
    [SerializeField] RectTransform quitDisplay;

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        graphicsTab.onClick.RemoveAllListeners();
        SettingsTab.onClick.RemoveAllListeners();
        controlsTab.onClick.RemoveAllListeners();
        saveTab.onClick.RemoveAllListeners();
        quitTab.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();

        graphicsTab.onClick.AddListener(OnClickGraphicsTab);
        SettingsTab.onClick.AddListener(OnClickSettingsTab);
        controlsTab.onClick.AddListener(OnClickControlsTab);
        saveTab.onClick.AddListener(OnClickSaveTab);
        quitTab.onClick.AddListener(OnClickQuitTab);

        graphicsTab.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
        SettingsTab.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
        controlsTab.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
        saveTab.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
        quitTab.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;

        exitButton.onClick.AddListener(OnExit);
        OnClickGraphicsTab();

        MenuSFXManager.Instance.PlayOptionsMenuOpen();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        MenuSFXManager.Instance.PlayOptionsMenuClose();
    }

    private void OnExit()
    {
        gameObject.SetActive(false);   
    }

    void OnTabEnter()
    {
        MenuSFXManager.Instance.PlayHover();
    }

    void ResetTabDisplays()
    {
        graphicsDisplay.gameObject.SetActive(false);
        settingsDisplay.gameObject.SetActive(false);
        controlsDisplay.gameObject.SetActive(false);
        saveDisplay.gameObject.SetActive(false);
        quitDisplay.gameObject.SetActive(false);
    }

    void OnClickGraphicsTab()
    {
        ResetTabDisplays();
        graphicsDisplay.gameObject.SetActive(true);
        MenuSFXManager.Instance.PlayClick();
    }

    void OnClickSettingsTab()
    {
        ResetTabDisplays();
        settingsDisplay.gameObject.SetActive(true);
        MenuSFXManager.Instance.PlayClick();
    }

    void OnClickControlsTab()
    {
        ResetTabDisplays();
        controlsDisplay.gameObject.SetActive(true);
        MenuSFXManager.Instance.PlayClick();
    }

    void OnClickSaveTab()
    {
        ResetTabDisplays();
        saveDisplay.gameObject.SetActive(true);
        MenuSFXManager.Instance.PlayClick();
    }

    void OnClickQuitTab()
    {
        ResetTabDisplays();
        quitDisplay.gameObject.SetActive(true);
        MenuSFXManager.Instance.PlayClick();
    }
}
