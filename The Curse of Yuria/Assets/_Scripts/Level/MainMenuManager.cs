using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Canvas
{
    public class MainMenuManager : MonoBehaviour
    {
        IGlobal global;

        public void Start()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            global.CloseAllDisplays();
            global.getTitleScreenDisplay.gameObject.SetActive(true);
            GameObject.Find("/DontDestroyOnLoad/AllieRoot").SetActive(false);
            
        }

        public void OnDestroy()
        {
            global.getTitleScreenDisplay.gameObject.SetActive(false);
            GameObject.Find("/DontDestroyOnLoad/AllieRoot").SetActive(true);
        }
    }
}