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
            load.onClick.RemoveAllListeners();
            mainMenu.onClick.RemoveAllListeners();
            quit.onClick.RemoveAllListeners();

            load.onClick.AddListener(RefreshFiles);
            mainMenu.onClick.AddListener(LoadMainMenu);
            quit.onClick.AddListener(Application.Quit);

            Global.instance.gameState = Global.GameState.Stopped;
            animator.SetTrigger("Activate");
            rightPanel.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Global.instance.gameState = Global.GameState.Playing;
        }

        void LoadMainMenu()
        {
            Global.instance.sceneIDToLoad = 2;
            Global.instance.scenePositionToStart = Vector2.zero;
            Global.instance.ToggleDisplay(Global.Display.Loading);
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
                button.onClick.AddListener(() => SaveManager.instance.OnLoad(fileInfo.Name));
                button.transform.GetChild(0).GetComponent<Text>().text = fileInfo.Name.Split('.')[0];
            }
        }
    }
}