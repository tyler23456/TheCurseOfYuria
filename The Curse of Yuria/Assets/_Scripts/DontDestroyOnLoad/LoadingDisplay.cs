using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TCOY.DontDestroyOnLoad
{
    public class LoadingDisplay : MonoBehaviour
    {
        IGlobal global;

        [SerializeField] Image SceneLoaderImage;
        [SerializeField] Slider progressBar; 

        private void OnEnable()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            IGlobal.gameState = IGlobal.GameState.Stopped;
            global.StartCoroutine(CoroutineLoad());
        }

        private void OnDisable()
        {
            IGlobal.gameState = IGlobal.GameState.Playing;
        }

        IEnumerator CoroutineLoad()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(global.sceneIDToLoad);
            float progress = 0f;

            while (!asyncOperation.isDone)
            {
                progress = asyncOperation.progress / 0.9f;
                yield return new WaitForEndOfFrame();
            }

            global.getCamera.gameObject.SetActive(false);
            global.allies.SetPosition(global.scenePositionToStart);
            global.allies.SetEulerAngleZ(global.sceneEulerAngleZToStart);
            global.getCamera.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}