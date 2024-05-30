using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System;


public class MainMenuDisplay : DisplayBase
{
    public static DisplayBase Instance { get; protected set; }

    [SerializeField] Button buttonPrefab;
    [SerializeField] RectTransform rightPanel;
    [SerializeField] RectTransform grid;

    [SerializeField] Button newGame;
    [SerializeField] Button load;
    [SerializeField] Button quit;

    Button button;

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Global.Instance.gameState = Global.GameState.Stopped;

        newGame.onClick.RemoveAllListeners();
        load.onClick.RemoveAllListeners();
        quit.onClick.RemoveAllListeners();

        newGame.onClick.AddListener(StartNewGame);
        load.onClick.AddListener(RefreshFiles);
        quit.onClick.AddListener(Application.Quit);

        rightPanel.gameObject.SetActive(false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    void StartNewGame()
    {
        SaveManager.instance.OnNewGame();
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
