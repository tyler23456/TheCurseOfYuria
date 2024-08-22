using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompanyLogoAnimators : MonoBehaviour
{
    bool hasLoaded = false;

    void Update()
    {
        if (hasLoaded)
            return;

        hasLoaded = true;

        //Transform loadingDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/LoadingDisplay").transform;
        //ILoadingData.sceneID = 1;
        //loadingDisplay.gameObject.SetActive(true);

        SceneManager.LoadSceneAsync(1);
    }
}
