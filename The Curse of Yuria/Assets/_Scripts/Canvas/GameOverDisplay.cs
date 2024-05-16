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
        IGlobal global;

        [SerializeField] Button buttonPrefab;
        [SerializeField] RectTransform rightPanel;
        [SerializeField] RectTransform grid;

        [SerializeField] Button load;
        [SerializeField] Button mainMenu;
        [SerializeField] Button quit;

        [SerializeField] Animator animator;

        Button button;

        void OnEnable()
        {
            saveManager = GameObject.Find("/DontDestroyOnLoad").GetComponent<ISaveManager>();
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

            load.onClick.AddListener(RefreshFiles);
            mainMenu.onClick.AddListener(LoadMainMenu);
            quit.onClick.AddListener(Application.Quit);

            IGlobal.gameState = IGlobal.GameState.Stopped;
            animator.SetTrigger("Activate");
            rightPanel.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            IGlobal.gameState = IGlobal.GameState.Playing;
        }

        void LoadMainMenu()
        {
            global.sceneIDToLoad = 2;
            global.scenePositionToStart = Vector2.zero;
            global.ToggleDisplay(IGlobal.Display.LoadingDisplay);
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
                button.onClick.AddListener(() => saveManager.OnLoad(fileInfo.Name));
                button.transform.GetChild(0).GetComponent<Text>().text = fileInfo.Name.Split('.')[0];
            }
        }
    }
}