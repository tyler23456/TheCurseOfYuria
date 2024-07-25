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
        GameStateManager.Instance.Stop();

        newGame.onClick.RemoveAllListeners();
        load.onClick.RemoveAllListeners();
        quit.onClick.RemoveAllListeners();

        newGame.onClick.AddListener(StartNewGame);
        load.onClick.AddListener(RefreshFiles);
        quit.onClick.AddListener(Quit);

        newGame.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
        load.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
        quit.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;

        rightPanel.gameObject.SetActive(false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    void OnTabEnter()
    {
        MenuSFXManager.Instance.PlayHover();
    }

    void OnItemEnter()
    {
        MenuSFXManager.Instance.PlayHover();
    }

    void OnItemClick(FileInfo fileInfo)
    {
        SaveManager.instance.OnLoad(fileInfo.Name);
        MenuSFXManager.Instance.PlayClick();
    }

    void StartNewGame()
    {
        SaveManager.instance.OnNewGame();
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
            button.onClick.AddListener(() => OnItemClick(fileInfo));
            button.transform.GetChild(0).GetComponent<Text>().text = fileInfo.Name.Split('.')[0];
            button.GetComponent<PointerHover>().onPointerEnter = OnItemEnter;
        }

        MenuSFXManager.Instance.PlayClick();
    }

    void Quit()
    {
        MenuSFXManager.Instance.PlayClick();
        Application.Quit();
    }
}
