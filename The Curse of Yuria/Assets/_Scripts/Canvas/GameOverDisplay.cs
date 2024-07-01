using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

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

        load.onClick.RemoveAllListeners();
        mainMenu.onClick.RemoveAllListeners();
        quit.onClick.RemoveAllListeners();

        load.onClick.AddListener(RefreshFiles);
        mainMenu.onClick.AddListener(LoadMainMenu);
        quit.onClick.AddListener(Application.Quit);

        animator.SetTrigger("Activate");
        rightPanel.gameObject.SetActive(false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    void LoadMainMenu()
    {
        LoadingDisplay.GetChild(0).name = "MainMenu";
        LoadingDisplay.gameObject.SetActive(true);
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
