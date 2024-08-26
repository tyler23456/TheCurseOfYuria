using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Level
{
    public class MainMenuManager : MonoBehaviour
    {

        public void Start()
        {
            GameObject.Find("/DontDestroyOnLoad/Canvas/MainMenuDisplay").SetActive(true);
        }
    }
}