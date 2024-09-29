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
    public class GameOverDisplay : DisplayBase
    {
        public static DisplayBase Instance { get; protected set; }

        [SerializeField] Transform LoadingDisplay;

        [SerializeField] Button buttonPrefab;
        [SerializeField] RectTransform rightPanel;
        [SerializeField] RectTransform grid;

        [SerializeField] Button load;
        [SerializeField] Button mainMenu;
        [SerializeField] Button quit;

        [SerializeField] Animator animator;

        Button button;

        public override void Initialize()
        {
            base.Initialize();
            Instance = this;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            GameStateManager.Instance.Stop();

            load.onClick.RemoveAllListeners();
            mainMenu.onClick.RemoveAllListeners();
            quit.onClick.RemoveAllListeners();

            load.onClick.AddListener(LoadSaves);
            mainMenu.onClick.AddListener(LoadMainMenu);
            quit.onClick.AddListener(Quit);

            load.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
            mainMenu.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
            quit.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;

            animator.SetTrigger("Activate");
            rightPanel.gameObject.SetActive(false);

            IBattleData.isGameOver = true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            IBattleData.isGameOver = false;
        }

        void OnTabEnter()
        {
            MenuSFXManager.Instance.PlayHover();
        }

        void OnItemEnter()
        {
            MenuSFXManager.Instance.PlayHover();
        }

        void LoadSaves()
        {
            RefreshFiles();
            MenuSFXManager.Instance.PlayClick();
        }

        void LoadMainMenu()
        {
            SaveManager.instance.ClearNonPersistentSceneData();

            ILoadingData.sceneID = 2;
            ILoadingData.destination = new Vector3(16.724180221557618f, -26.64665985107422f, 0f);
            LoadingDisplay.gameObject.SetActive(true);
            MenuSFXManager.Instance.PlayClick();
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
                button.GetComponent<PointerHover>().onPointerEnter = OnItemEnter;
            }
        }

        void Quit()
        {
            Application.Quit();
            MenuSFXManager.Instance.PlayClick();
        }
    }
}
