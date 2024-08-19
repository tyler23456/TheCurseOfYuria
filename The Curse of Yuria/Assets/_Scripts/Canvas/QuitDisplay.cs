using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    public class QuitDisplay : MonoBehaviour
    {
        [SerializeField] Transform loadingDisplay;

        [SerializeField] GameObject leftPanel;
        [SerializeField] GameObject rightPanel;
        [SerializeField] RectTransform rightGrid;
        [SerializeField] Text heading;
        [SerializeField] Text description;

        [SerializeField] Button returnToMenu; 

        InventoryUI inventory;

        void OnEnable()
        {
            leftPanel.SetActive(false);
            rightPanel.SetActive(false);

            returnToMenu.onClick.RemoveAllListeners();

            returnToMenu.onClick.AddListener(OnReturnToMenu);

            returnToMenu.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;

            heading.text = "";
            description.text = "";
        }

        void OnReturnToMenu()
        {
            ILoadingData.destination = Vector2.zero;
            ILoadingData.eulerAngleZ = 0f;
            ILoadingData.sceneID = 2;

            loadingDisplay.gameObject.SetActive(true);
        }

        void OnTabEnter()
        {
            MenuSFXManager.Instance.PlayHover();
        }
    }
}