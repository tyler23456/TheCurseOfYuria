using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingDisplay : DisplayBase
{
    public static LoadingDisplay Instance { get; protected set; }

    [SerializeField] GameObject mainCamera;
    [SerializeField] Image SceneLoaderImage;
    [SerializeField] Slider progressBar;

    public int sceneIDToLoad = 0;
    public Vector2 positionToStart = Vector2.zero;
    public float eulerAngleZToStart = 0f;
    public bool isSettingTransforms = true;

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameStateManager.Instance.Stop();
        StartCoroutine(CoroutineLoad());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    IEnumerator CoroutineLoad()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIDToLoad);
        float progress = 0f;

        while (!asyncOperation.isDone)
        {
            progress = asyncOperation.progress / 0.9f;
            yield return new WaitForEndOfFrame();
        }

        StatsDisplay.Instance.HideAllInParent();
        StatsDisplay.Instance.ShowAllInParent();
        AllieManager.Instance.Refresh();
        gameObject.SetActive(false);

        if (!isSettingTransforms)
            yield break;

        mainCamera.SetActive(false);
        AllieManager.Instance.SetPosition(positionToStart);
        AllieManager.Instance.SetEulerAngleZ(eulerAngleZToStart);
        mainCamera.SetActive(true);
    }

    public void ShowExclusivelyInParent(int sceneIDToLoad, Vector2 positionToStart, float eulerAngleZToStart)
    {
        this.sceneIDToLoad = sceneIDToLoad;
        this.positionToStart = positionToStart;
        this.eulerAngleZToStart = eulerAngleZToStart;
        isSettingTransforms = true;

        base.ShowExclusivelyInParent();  
    }

    public void ShowExclusivelyInParent(int sceneIDToLoad)
    {
        this.sceneIDToLoad = sceneIDToLoad;
        isSettingTransforms = false;

        base.ShowExclusivelyInParent();
    }
}
