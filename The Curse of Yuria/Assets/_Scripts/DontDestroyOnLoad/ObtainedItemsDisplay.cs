using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    public class ObtainedItemsDisplay : MonoBehaviour
    {
        IGlobal global;

        [SerializeField] Transform grid;
        [SerializeField] GameObject obtainedItemPrefab;
        [SerializeField] float displayTime = 10f;

        void OnEnable()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

            for (int i = 0; i < global.obtainedItems.Count; i++)
            {
                GameObject obj = Instantiate(obtainedItemPrefab, grid);
                obj.GetComponent<Text>().text = global.obtainedItems.Dequeue();
                Destroy(obj, displayTime);
            }
        }

        void OnDisable()
        {
            foreach (Transform t in grid)
                Destroy(t.gameObject);
        }

        void Update()
        {
            if (grid.childCount == 0)
                gameObject.SetActive(false);
        }
    }
}