using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TCOY.DontDestroyOnLoad
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        IGlobal global;

        [SerializeField] Image SceneLoaderImage;
        [SerializeField] Slider progressBar; 
        [SerializeField] float fadeTime = 2f;

        void Start()
        {
            global = GetComponent<IGlobal>();
        }

        public void Load(int level, Vector2 position, float eulerAngleZ)
        {
            global.StartCoroutine(CoroutineLoad(level, position, eulerAngleZ));
        }

        IEnumerator FadeScreenColorAlphaTo(float targetValue)
        {
            float startTime = Time.time;
            Color color;
            while (Time.time < startTime + fadeTime)
            {
                color = SceneLoaderImage.color;
                color.a = Mathf.Lerp(color.a, targetValue, 0.1f);
                SceneLoaderImage.color = color;
                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator CoroutineLoad(int level, Vector2 position, float eulerAngleZ)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(level);
            float progress = 0f;

            SceneLoaderImage.gameObject.SetActive(true);
            FadeScreenColorAlphaTo(1f);
            progressBar.gameObject.SetActive(true);

            while (!asyncOperation.isDone)
            {
                progress = asyncOperation.progress / 0.9f;
                yield return null;
            }

            MoveParty(position, eulerAngleZ);

            progressBar.gameObject.SetActive(false);
            FadeScreenColorAlphaTo(0f);
            SceneLoaderImage.gameObject.SetActive(false);

            //loading screen fade out;
        }

        public void MoveParty(Vector2 position, float eulerAngleZ)
        {
            foreach (Transform partyMember in global.getPartyRoot.transform)
            {
                partyMember.gameObject.SetActive(false);
                partyMember.gameObject.transform.position = new Vector3(position.x, position.y, 0f);
                partyMember.gameObject.transform.eulerAngles = new Vector3(0f, 0f, eulerAngleZ);
                partyMember.gameObject.SetActive(true);
            }
        }
    }
}