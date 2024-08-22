using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingDisplay : DisplayBase
{
    public static LoadingDisplay Instance { get; protected set; }

    [SerializeField] Transform allies;
    [SerializeField] Transform enemies;

    [SerializeField] GameObject mainCamera;
    [SerializeField] Image SceneLoaderImage;
    [SerializeField] Slider progressBar;

    Transform t;

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        for (int i = enemies.childCount - 1; i >= 0; i--)
        {
            t = enemies.GetChild(i);
            t.parent = null;
            Destroy(t.gameObject);
        }

        GameStateManager.Instance.Stop();
        StartCoroutine(CoroutineLoad());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    IEnumerator CoroutineLoad()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(ILoadingData.sceneID);
        float progress = 0f;

        while (!asyncOperation.isDone)
        {
            progress = asyncOperation.progress / 0.9f;
            yield return new WaitForEndOfFrame();
        }

        int count = Mathf.Min(allies.childCount, IAllie.MaxActiveAlliesCount);

        for (int i = 1; i < count; i++)
        {
            IController controller = allies.GetChild(i).GetComponent<IController>();
            controller.ResetToDefault();
        }
        gameObject.SetActive(false);
    }
}
