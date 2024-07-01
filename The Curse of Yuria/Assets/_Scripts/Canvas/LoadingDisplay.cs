using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingDisplay : DisplayBase
{
    public static LoadingDisplay Instance { get; protected set; }

    [SerializeField] Transform allies;

    [SerializeField] GameObject mainCamera;
    [SerializeField] Image SceneLoaderImage;
    [SerializeField] Slider progressBar;

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
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(transform.GetChild(0).name);
        float progress = 0f;

        while (!asyncOperation.isDone)
        {
            progress = asyncOperation.progress / 0.9f;
            yield return new WaitForEndOfFrame();
        }

        gameObject.SetActive(false);
    }
}
