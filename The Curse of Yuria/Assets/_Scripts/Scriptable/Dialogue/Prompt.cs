using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Prompt : ActionBase, IAction
{
    [Range(3, 10)] [SerializeField] string text;

    /*public override IEnumerator Activate(IGlobal global, IFactory factory, List<IActor> actors)
    {
        base.Activate(global, factory, actors);

        Image displayImage = global.getPromptDisplayImage;
        TMP_Text displayText = global.getPromptDisplayText;
        displayText.text = text;
        displayText.maxVisibleCharacters = 0;
        //set up display

        global.getPromptDisplay.gameObject.SetActive(true);

        while (displayText.maxVisibleCharacters != displayText.text.Length || !Input.GetKeyDown(KeyCode.Mouse0))
            for (int i = 0; i < text.Length; i++)
            {
                displayText.maxVisibleCharacters++;
                yield return new WaitForSecondsRealtime(0.02f);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                    displayText.maxVisibleCharacters = displayText.text.Length;
            }

        global.getPromptDisplay.gameObject.SetActive(false);
    }*/
}
