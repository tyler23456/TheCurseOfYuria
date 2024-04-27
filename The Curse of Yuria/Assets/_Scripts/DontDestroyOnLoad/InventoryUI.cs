using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace TCOY.DontDestroyOnLoad
{
    public class InventoryUI : MonoBehaviour, IInventoryUI
    {
        IFactory factory;

        [SerializeField] Button buttonPrefab;

        public bool isVertical { get; set; } = false;
        public Vector2Int padding { get; set; } = new Vector2Int(5, 5);
        public Vector2Int windowSize { get; set; } = Vector2Int.zero;
        public Action<string> OnClick { get; set; } = (info) => { };
        public RectTransform buttonParent { get; set; } = null;
        public IInventory inventory { get; set; } = null;

        public List<Button> icons { get; set; } = new List<Button>();
        Button icon = null;

        public void Start()
        {
            factory = GetComponent<IFactory>();
        }

        public void Show()
        {
            if (isVertical)
                DisplayVertically();
            else
                DisplayHorizonally();           
        }

        public void DisplayVertically()
        {
            Vector2 origin = ((RectTransform)buttonPrefab.transform).anchoredPosition;
            int maxRows = (int)(windowSize.y / (((RectTransform)buttonPrefab.transform).sizeDelta.y + padding.y));
            int row = 0;
            int column = 0;

            icons.Clear();

            for (int i = 0; i < inventory.count; i++)
            {
                icon = Instantiate(buttonPrefab);
                icon.onClick.AddListener(() => { OnClick(inventory.GetName(i)); });
                RectTransform rectTransform = (RectTransform)icon.transform;
                rectTransform.anchoredPosition = new Vector2(origin.x + row * windowSize.x, origin.y + column * windowSize.y);
                //need to set the image
                icon.image.sprite = factory.GetIcon(inventory.GetName(i)).Sprite;

                row++;
                if (row >= maxRows)
                {
                    row = 0;
                    column++;
                }
            }
        }

        public void DisplayHorizonally()
        {
            Vector2 origin = ((RectTransform)buttonPrefab.transform).anchoredPosition;
            int maxColumns = (int)(windowSize.x / (((RectTransform)buttonPrefab.transform).sizeDelta.x + padding.x));
            int row = 0;
            int column = 0;

            for (int i = 0; i < inventory.count; i++)
            {
                Button button = Instantiate(buttonPrefab);
                button.onClick.AddListener(() => { OnClick(inventory.GetName(i)); });
                RectTransform rectTransform = (RectTransform)button.transform;
                rectTransform.anchoredPosition = new Vector2(origin.x + row * windowSize.x, origin.y + column * windowSize.y);
                //need to set the image

                column++;
                if (column >= maxColumns)
                {
                    column = 0;
                    row++;
                }                
            }
        }


    }
}