using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayBase : MonoBehaviour
{
    [SerializeField] Transform displayTransform;

    public virtual void Initialize()
    {
    }

    protected virtual void OnEnable()
    {
        foreach (Transform child in displayTransform.parent)
            child.gameObject.SetActive(false);

        displayTransform.gameObject.SetActive(true);

        GameStateManager.Instance.Pause();
        MarkerManager.instance.DestroyAllMarkers();
        //Global.instance.getAudioSource.PlayOneShot(open);
    }

    protected virtual void OnDisable()
    {
        GameStateManager.Instance.Play();
        MarkerManager.instance.DestroyAllMarkers();

        displayTransform.gameObject.SetActive(false);
        //Global.instance.getAudioSource.PlayOneShot(open);
    }
}
