using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TCOY.ScriptedSequencers
{
    public class DialogueSFXManager : MonoBehaviour
    {
        public static DialogueSFXManager Instance { get; private set; }

        [SerializeField] AssetLabelReference dialogueReference;

        [SerializeField] AudioSourceManager audioSourceManager;

        List<AudioClip> dialogueSFXs = new List<AudioClip>();

        void Awake()
        {
            Instance = this;

            Addressables.LoadAssetsAsync<AudioClip>(dialogueReference, (i) =>
            {
                dialogueSFXs.Add(i);

            }).WaitForCompletion();
        }

        public void PlayDialogueSFX()
        {
            audioSourceManager.PlayDialogue(dialogueSFXs, 0.4f, 0.6f, 0.8f, 1.2f);
        }

        public void StopDialogueSFX()
        {
            audioSourceManager.StopDialogue();
        }
    }
}
