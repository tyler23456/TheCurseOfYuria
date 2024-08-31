using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TCOY.ScriptedSequencers
{
    [CreateAssetMenu(fileName = "NewPrompt", menuName = "Cutscene/Prompt")]
    public class Prompt : ActionBase, ICutsceneAction
    {
        static List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };

        static bool hasReferences = false;
        static Transform scriptedSequencerDisplay;
        static Camera camera;
        static Text textName;
        static TMP_Text textBody;

        [TextArea(3, 10)] [SerializeField] string text;

        public override IEnumerator Activate()
        {
            if (!hasReferences)
                InitializeReferences();

            textName.text = characterName.name;
            textBody.text = this.text;
            textBody.maxVisibleCharacters = 0;
            onStart.Invoke();

            Transform t = characterName.gameObject.transform;

            UpdateRenderTextureCamera(camera, t);

            while (textBody.maxVisibleCharacters < textBody.text.Length || !Input.GetKeyDown(KeyCode.Mouse0))
            {
                onUpdate.Invoke();
                textBody.maxVisibleCharacters++;

                if (textBody.maxVisibleCharacters <= textBody.text.Length && !vowels.Contains(textBody.text[textBody.maxVisibleCharacters - 1]))
                    DialogueSFXManager.Instance.PlayDialogueSFX();

                yield return null;

                if (Input.GetKeyDown(KeyCode.Mouse1))
                    textBody.maxVisibleCharacters = textBody.text.Length - 1;

                if (textBody.maxVisibleCharacters == textBody.text.Length - 1)
                    onStop.Invoke();
            }

            DialogueSFXManager.Instance.StopDialogueSFX();
            onFinish.Invoke();

            onStart = () => { };
            onUpdate = () => { };
            onStop = () => { };
            onFinish = () => { };
        }

        void InitializeReferences()
        {
            scriptedSequencerDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/ScriptedSequencerDisplay").transform;

            camera = scriptedSequencerDisplay.GetChild(1).GetComponent<Camera>();
            textName = scriptedSequencerDisplay.GetChild(0).GetChild(0).GetComponent<Text>();
            textBody = scriptedSequencerDisplay.GetChild(0).GetChild(1).GetComponent<TMP_Text>();

            hasReferences = true;
        }
        void UpdateRenderTextureCamera(Camera camera, Transform t)
        {
            if (t != null)
            {
                camera.cullingMask = (1 << t.GetChild(0).gameObject.layer)
                | (1 << LayerMask.NameToLayer("Light"));

                if (t.GetChild(0).eulerAngles.y == 0f)
                {
                    camera.transform.position = t.position + new Vector3(0f, 1f, -3f);
                    camera.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                else
                {
                    camera.transform.position = t.position + new Vector3(0f, 1f, 3f);
                    camera.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }
            }
        }
    }
}
