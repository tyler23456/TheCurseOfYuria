using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayBase : MonoBehaviour
{
    public virtual void Initialize()
    {
    }

    protected virtual void OnEnable()
    {
        Global.Instance.gameState = Global.GameState.Paused;
        MarkerManager.instance.DestroyAllMarkers();
        //Global.instance.getAudioSource.PlayOneShot(open);
    }

    protected virtual void OnDisable()
    {
        Global.Instance.gameState = Global.GameState.Playing;
        MarkerManager.instance.DestroyAllMarkers();
        //Global.instance.getAudioSource.PlayOneShot(open);
    }

    public void ShowExclusivelyInParent()
    {
        HideAllInParent();
        Show();
    }

    public void ShowAllInParent()
    {
        foreach (Transform child in transform.parent)
            child.gameObject.SetActive(true);
    }

    public void HideAllInParent()
    {
        foreach (Transform child in transform.parent)
            child.gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Refresh()
    {
        Hide();
        Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ToggleExclusivelyInParent()
    {
        if (gameObject.activeSelf)
            HideAllInParent();
        else
            ShowExclusivelyInParent();
    }
}
