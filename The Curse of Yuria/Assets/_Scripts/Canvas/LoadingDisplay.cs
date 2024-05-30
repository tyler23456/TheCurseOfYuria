using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingDisplay : DisplayBase
{
    public static LoadingDisplay Instance { get; protected set; }

    [SerializeField] Image SceneLoaderImage;
    [SerializeField] Slider progressBar;

    public int sceneIDToLoad = 0;
    public Vector3 positionToStart = Vector3.zero;
    public float eulerAngleZToStart = 0f;

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Global.Instance.gameState = Global.GameState.Stopped;
        Global.Instance.StartCoroutine(CoroutineLoad());
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
        Global.Instance.getCamera.gameObject.SetActive(false);
        Global.Instance.allies.SetPosition(positionToStart);
        Global.Instance.allies.SetEulerAngleZ(eulerAngleZToStart);
        Global.Instance.getCamera.gameObject.SetActive(true);
        StatsDisplay.Instance.ShowAllInParent();
        gameObject.SetActive(false);     
    }

    public void ShowExclusivelyInParent(int sceneIDToLoad, Vector2 positionToStart = new Vector2(), float eulerAngleZToStart = 0f)
    {
        this.sceneIDToLoad = sceneIDToLoad;
        this.positionToStart = positionToStart;
        this.eulerAngleZToStart = eulerAngleZToStart;

        base.ShowExclusivelyInParent();  
    }
}
