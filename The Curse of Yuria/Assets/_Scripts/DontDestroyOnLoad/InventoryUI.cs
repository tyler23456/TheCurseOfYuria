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

        public RectTransform grid { get; set; } = null;
        public Action<string> OnClick { get; set; } = (info) => { };
        public IInventory inventory { get; set; } = null;

        Button button = null;

        public void Start()
        {
            factory = GetComponent<IFactory>();
        }

        public void Show()
        {
            Display();        
        }

        void Display()
        {
            EmptyDisplay();

            for (int i = 0; i < inventory.count; i++)
            {   
                int index = i;
                button = Instantiate(buttonPrefab, grid);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => 
                { 
                    OnClick(inventory.GetName(index));
                });
                button.transform.GetChild(1).GetComponent<Image>().sprite = factory.GetItem(inventory.GetName(i)).icon;
                button.transform.GetChild(2).GetComponent<Text>().text = inventory.GetCount(i).ToString();

            }
        }

        public void EmptyDisplay()
        {
            if (grid == null)
                return;

            foreach (RectTransform child in grid)
                Destroy(child.gameObject);
        }


    }
}