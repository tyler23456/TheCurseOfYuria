using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    public static LightManager instance { get; set; }

    [SerializeField] Light2D light2D;
    [SerializeField] Animator animator;

    private void Awake()
    {
        instance = this;
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    
}
