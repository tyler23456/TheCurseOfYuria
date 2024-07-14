using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayBase : MonoBehaviour
{
    protected AudioClip open;
    protected AudioClip close;
    protected AudioClip hover;
    protected AudioClip click;
    protected AudioClip equip;
    protected AudioClip unequip;



    [SerializeField] Transform displayTransform;

    public virtual void Initialize()
    {
    }

    protected virtual void OnEnable()
    {
        foreach (Transform child in displayTransform.parent)
            if (child != displayTransform)
                child.gameObject.SetActive(false);

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
