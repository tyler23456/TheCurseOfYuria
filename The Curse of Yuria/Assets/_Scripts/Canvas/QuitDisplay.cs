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
            SaveManager.instance.ClearNonPersistentSceneData();

            ILoadingData.sceneID = 2;
            ILoadingData.destination = new Vector3(16.724180221557618f, -26.64665985107422f, 0f);
            loadingDisplay.gameObject.SetActive(true);
        }

        void OnTabEnter()
        {
            MenuSFXManager.Instance.PlayHover();
        }
    }
}