using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    public class OptionsDisplay : MonoBehaviour
    {
        [SerializeField] Button graphicsTab;
        [SerializeField] Button SettingsTab;
        [SerializeField] Button controlsTab;
        [SerializeField] Button quitTab;

        [SerializeField] RectTransform graphicsDisplay;
        [SerializeField] RectTransform settingsDisplay;
        [SerializeField] RectTransform controlsDisplay;
        [SerializeField] RectTransform quitDisplay;  

        void Start()
        {
            graphicsTab.onClick.AddListener(OnClickGraphicsTab);
            SettingsTab.onClick.AddListener(OnClickSettingsTab);
            controlsTab.onClick.AddListener(OnClickControlsTab);
            quitTab.onClick.AddListener(OnClickQuitTab);
            OnClickGraphicsTab();

        }

        void ResetTabDisplays()
        {
            graphicsDisplay.gameObject.SetActive(false);
            settingsDisplay.gameObject.SetActive(false);
            controlsDisplay.gameObject.SetActive(false);
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

        void OnClickQuitTab()
        {
            ResetTabDisplays();
            quitDisplay.gameObject.SetActive(true);
        }
    }
}