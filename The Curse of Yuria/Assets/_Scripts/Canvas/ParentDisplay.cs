using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDisplay : MonoBehaviour
{
    DisplayBase display;
    void Awake()
    {
        foreach (Transform child in transform)
        {
            display = child.GetComponent<DisplayBase>();

            if (display == null)
                continue;

            display.Initialize();
        }
            
    }
}
