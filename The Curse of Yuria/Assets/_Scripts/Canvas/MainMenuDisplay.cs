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
    public class MainMenuDisplay : MonoBehaviour
    {
        ISaveManager saveManager;

        [SerializeField] Button buttonPrefab;
        [SerializeField] RectTransform rightPanel;
        [SerializeField] RectTransform grid;

        [SerializeField] Button newGame;
        [SerializeField] Button load;
        [SerializeField] Button quit;

        Button button;

        void OnEnable()
        {
            saveManager = GameObject.Find("/DontDestroyOnLoad").GetComponent<ISaveManager>();

            newGame.onClick.RemoveAllListeners();
            load.onClick.RemoveAllListeners();
            quit.onClick.RemoveAllListeners();

            newGame.onClick.AddListener(StartNewGame);
            load.onClick.AddListener(RefreshFiles);
            quit.onClick.AddListener(Application.Quit);

            rightPanel.gameObject.SetActive(false);
            //optional animation here

            Global.instance.gameState = Global.GameState.Stopped;
        }

        void OnDisable()
        {
            Global.instance.gameState = Global.GameState.Playing;
        }

        void StartNewGame()
        {
            saveManager.OnNewGame();
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