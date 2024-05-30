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

        graphicsTab.onClick.AddListener(OnClickGraphicsTab);
        SettingsTab.onClick.AddListener(OnClickSettingsTab);
        controlsTab.onClick.AddListener(OnClickControlsTab);
        saveTab.onClick.AddListener(OnClickSaveTab);
        quitTab.onClick.AddListener(OnClickQuitTab);
        OnClickGraphicsTab();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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
    }

    void OnClickSettingsTab()
    {
        ResetTabDisplays();
        settingsDisplay.gameObject.SetActive(true);
    }

    void OnClickControlsTab()
    {
        ResetTabDisplays();
        controlsDisplay.gameObject.SetActive(true);
    }

    void OnClickSaveTab()
    {
        ResetTabDisplays();
        saveDisplay.gameObject.SetActive(true);
    }

    void OnClickQuitTab()
    {
        ResetTabDisplays();
        quitDisplay.gameObject.SetActive(true);
    }
}
