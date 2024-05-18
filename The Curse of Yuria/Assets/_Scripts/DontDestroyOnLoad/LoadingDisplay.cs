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
                yield return null;
            }

            MoveParty(global.scenePositionToStart, global.sceneEulerAngleZToStart);

            gameObject.SetActive(false);
        }

        public void MoveParty(Vector2 position, float eulerAngleZ)
        {
            foreach (Transform partyMember in global.getAllieRoot)
            {
                partyMember.gameObject.SetActive(false);
                partyMember.gameObject.transform.position = new Vector3(position.x, position.y, 0f);
                partyMember.gameObject.transform.eulerAngles = new Vector3(0f, 0f, eulerAngleZ);
                partyMember.gameObject.SetActive(true);
            }
        }
    }
}