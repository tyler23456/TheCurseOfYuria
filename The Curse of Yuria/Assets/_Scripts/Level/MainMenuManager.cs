using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Canvas
{
    public class MainMenuManager : MonoBehaviour
    {

        public void Start()
        {
            Global.instance.ToggleDisplay(Global.Display.MainMenu);           
        }
    }
}