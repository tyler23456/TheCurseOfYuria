using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[CreateAssetMenu(fileName = "NewPrompt", menuName = "Cutscene/Prompt")]
public class Prompt : ActionBase, IAction
{
    [TextArea(3, 10)] [SerializeField] string text;

    public override IEnumerator Activate(List<IActor> actors, Image image, TMP_Text text)
    {
        text.text = this.text;
        text.maxVisibleCharacters = 0;
        onStart.Invoke();

        while (text.maxVisibleCharacters < text.text.Length || !Input.GetKeyDown(KeyCode.Mouse0))
        {
            onUpdate.Invoke();
            text.maxVisibleCharacters++;
            yield return null;
            
            if (Input.GetKeyDown(KeyCode.Mouse1))
                text.maxVisibleCharacters = text.text.Length - 1;

            if (text.maxVisibleCharacters == text.text.Length - 1)
                onStop.Invoke();
        }

        onFinish.Invoke();

        onStart = () => { };
        onUpdate = () => { };
        onStop = () => { };
        onFinish = () => { };
    }
}
