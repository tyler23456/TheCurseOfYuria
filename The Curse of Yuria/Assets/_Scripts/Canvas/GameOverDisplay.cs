using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

namespace TCOY.Canvas
{
    public class GameOverDisplay : MonoBehaviour
    {
        ISaveManager saveManager;
        ISceneLoader sceneLoader;

        [SerializeField] Button buttonPrefab;
        [SerializeField] RectTransform rightPanel;
        [SerializeField] RectTransform grid;

        [SerializeField] Button load;
        [SerializeField] Button mainMenu;
        [SerializeField] Button quit;

        [SerializeField] Animator animator;

        Button button;

        void Start()
        {
            saveManager = GameObject.Find("/DontDestroyOnLoad").GetComponent<ISaveManager>();
            sceneLoader = GameObject.Find("/DontDestroyOnLoad").GetComponent<ISceneLoader>();
            load.onClick.AddListener(RefreshFiles);
            mainMenu.onClick.AddListener(() => { sceneLoader.Load(2, Vector2.zero, 0f); gameObject.SetActive(false); });
            quit.onClick.AddListener(Application.Quit);
        }

        void OnEnable()
        {
            Time.timeScale = 0f;
            animator.SetTrigger("Activate");
            rightPanel.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }

        void RefreshFiles()
        {
            rightPanel.gameObject.SetActive(true);

            if (grid == null)
                return;

            foreach (RectTransform child in grid)
                Destroy(child.gameObject);

            DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath + Path.AltDirectorySeparatorChar);
            FileInfo[] fileInfos = info.GetFiles();


            foreach (FileInfo fileInfo in fileInfos)
            {
                button = Instantiate(buttonPrefab, grid);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    saveManager.OnLoad(fileInfo.Name);
                    gameObject.SetActive(false);
                });
                button.transform.GetChild(0).GetComponent<Text>().text = fileInfo.Name.Split('.')[0];
            }
        }
    }
}