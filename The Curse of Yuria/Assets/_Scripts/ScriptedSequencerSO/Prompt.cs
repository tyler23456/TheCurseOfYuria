using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[CreateAssetMenu(fileName = "NewPrompt", menuName = "Cutscene/Prompt")]
public class Prompt : ActionBase, ICutsceneAction
{
    [TextArea(3, 10)] [SerializeField] string text;

    public override IEnumerator Activate()
    {
        Camera camera = CutsceneDisplay.Instance.getCamera;
        Text textName = CutsceneDisplay.Instance.getPromptName;
        TMP_Text textBody = CutsceneDisplay.Instance.getPromptText;

        textName.text = characterName.name;
        textBody.text = this.text;
        textBody.maxVisibleCharacters = 0;
        onStart.Invoke();

        Transform t = NPCManager.instance.Find(characterName.name);

        UpdateRenderTextureCamera(camera, t);
        
        while (textBody.maxVisibleCharacters < textBody.text.Length || !Input.GetKeyDown(KeyCode.Mouse0))
        {
            onUpdate.Invoke();
            textBody.maxVisibleCharacters++;
            yield return null;
            
            if (Input.GetKeyDown(KeyCode.Mouse1))
                textBody.maxVisibleCharacters = textBody.text.Length - 1;

            if (textBody.maxVisibleCharacters == textBody.text.Length - 1)
                onStop.Invoke();
        }

        onFinish.Invoke();

        onStart = () => { };
        onUpdate = () => { };
        onStop = () => { };
        onFinish = () => { };
    }

    void UpdateRenderTextureCamera(Camera camera, Transform t)
    {
        if (t != null)
        {
            camera.cullingMask = (1 << t.GetChild(0).gameObject.layer)
            | (1 << LayerMask.NameToLayer("Light"));

            if (t.GetChild(0).eulerAngles.y == 0f)
            {
                camera.transform.position = t.position + new Vector3(0f, 1f, -2f);
                camera.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                camera.transform.position = t.position + new Vector3(0f, 1f, 2f);
                camera.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
    }
}
