using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Canvas
{
    public class MainMenuManager : MonoBehaviour
    {

        public void Start()
        {
            GameObject.Find("/DontDestroyOnLoad/Canvas/MainMenuDisplay").SetActive(true);
            Transform allies = GameObject.Find("/DontDestroyOnLoad/Allies").transform;
            Transform camera = GameObject.Find("/DontDestroyOnLoad/Main Camera").transform;

            allies.gameObject.SetActive(false);

            bool previousActive = false;
            foreach (Transform allie in allies)
            {
                previousActive = allie.gameObject.activeSelf;
                allie.position = transform.GetChild(0).position;
                allie.eulerAngles = transform.GetChild(0).eulerAngles;
            }
            camera.transform.position = allies.GetChild(0).transform.position + new Vector3(0f, 0f, -1f);
        }
    }
}