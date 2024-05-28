using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    public class LoadingDisplay : MonoBehaviour
    {
        [SerializeField] Image SceneLoaderImage;
        [SerializeField] Slider progressBar;

        private void OnEnable()
        {;
            Global.instance.gameState = Global.GameState.Stopped;
            Global.instance.StartCoroutine(CoroutineLoad());
        }

        private void OnDisable()
        {
            Global.instance.gameState = Global.GameState.Playing;
        }

        IEnumerator CoroutineLoad()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(Global.instance.sceneIDToLoad);
            float progress = 0f;

            while (!asyncOperation.isDone)
            {
                progress = asyncOperation.progress / 0.9f;
                yield return new WaitForEndOfFrame();
            }

            //GameObject obj = GameObject.Find("/DontDestroyOnLoad/Pathfinding").gameObject;
            //obj.SetActive(false);
            Global.instance.getDisplay2.gameObject.SetActive(false);
            Global.instance.getCamera.gameObject.SetActive(false);
            Global.instance.allies.SetPosition(Global.instance.scenePositionToStart);
            Global.instance.allies.SetEulerAngleZ(Global.instance.sceneEulerAngleZToStart);
            Global.instance.getCamera.gameObject.SetActive(true);
            gameObject.SetActive(false);
            Global.instance.getDisplay2.gameObject.SetActive(true);
            //obj.SetActive(true);
        }
    }
}