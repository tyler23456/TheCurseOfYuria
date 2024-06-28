using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDisplay : MonoBehaviour
{
    DisplayBase[] displays;
    void Awake()
    {
        foreach (Transform child in transform)
        {
            displays = child.GetComponents<DisplayBase>();

            foreach (DisplayBase display in displays)
                if (display != null)
                    display.Initialize();
        }
    }
}
