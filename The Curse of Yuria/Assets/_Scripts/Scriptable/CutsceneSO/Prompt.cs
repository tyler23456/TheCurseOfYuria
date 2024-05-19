using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "NewPrompt", menuName = "Dialogue/Prompt")]
public class Prompt : ActionBase, IAction
{
    [Range(3, 10)] [SerializeField] string text;

    public override IEnumerator Activate(IGlobal global, IFactory factory, List<IActor> actors)
    {
        Image image = global.getPromptImage;
        TMP_Text text = global.getPromptText;
        text.maxVisibleCharacters = 0;

        while (text.maxVisibleCharacters != text.text.Length || !Input.GetKeyDown(KeyCode.Mouse0))
            for (int i = 0; i < text.text.Length; i++)
            {
                text.maxVisibleCharacters++;
                yield return new WaitForSecondsRealtime(0.02f);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                    text.maxVisibleCharacters = text.text.Length;
            }
    }
}
