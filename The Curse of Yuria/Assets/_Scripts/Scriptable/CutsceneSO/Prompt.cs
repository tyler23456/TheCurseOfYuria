using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "NewPrompt", menuName = "Cutscene/Prompt")]
public class Prompt : ActionBase, IAction
{
    [TextArea(3, 10)] [SerializeField] string text;

    public override IEnumerator Activate(IGlobal global, IFactory factory, List<IActor> actors)
    {
        Image image = global.getPromptImage;
        TMP_Text text = global.getPromptText;
        text.text = this.text;
        text.maxVisibleCharacters = 0;

        while (text.maxVisibleCharacters < text.text.Length || !Input.GetKeyDown(KeyCode.Mouse0))
        {
            text.maxVisibleCharacters++;
            yield return null;
            
            if (Input.GetKeyDown(KeyCode.Mouse1))
                text.maxVisibleCharacters = text.text.Length;
        }
    }
}
