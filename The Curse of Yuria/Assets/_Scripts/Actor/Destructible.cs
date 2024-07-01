using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class Destructible : Actor
    {
        AudioSource audioSource;
        Animator animator;
        IInteractable[] containers;
        IEnabler[] scriptsToBeEnabled;

        protected new void Awake()
        {
            base.Awake();

            animator = transform.GetComponent<Animator>();
            audioSource = transform.GetComponent<AudioSource>();

            containers = GetComponents<IInteractable>();
            scriptsToBeEnabled = GetComponents<IEnabler>();

            stats.onZeroHealth += () => animator.enabled = true;
            stats.onZeroHealth += () =>
            {
                audioSource?.Play();
                animator?.SetTrigger("Activate");

                Transform allies = GameObject.Find("/DontDestroyOnLoad/Allies").transform;
                IActor leader = allies.GetChild(0).GetComponent<IActor>();
                foreach (IInteractable container in containers)
                    container.Interact(leader);

                foreach (IEnabler scriptToBeEnabled in scriptsToBeEnabled)
                    scriptToBeEnabled.enabled = true;
            };
        }

        protected new void Update()
        {
            base.Update();
        }
    }
}