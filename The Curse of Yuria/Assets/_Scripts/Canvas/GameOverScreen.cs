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
    public class GameOverScreen : MonoBehaviour
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
            mainMenu.onClick.AddListener(() => sceneLoader.Load(2, Vector2.zero, 0f));
            quit.onClick.AddListener(Application.Quit);
        }

        void OnEnable()
        {
            animator.SetTrigger("Activate");
            rightPanel.gameObject.SetActive(false);
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
                });
                button.transform.GetChild(0).GetComponent<Text>().text = fileInfo.Name.Split('.')[0];
            }
        }
    }
}