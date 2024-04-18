using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System;
using TMPro;
using UnityEngine.UI;

namespace TCOY.DontDestroyOnLoad
{
    public class Cutscene : MonoBehaviour, ICutscene
    {
        [SerializeField] RectTransform promptPanel;
        [SerializeField] Image promptIcon;
        [SerializeField] TMP_Text promptText;

        Queue<string> actions;

        public bool waitingForInput { get; set; }
        public Action onStart { get; set; } = () => { };
        public Action onStop { get; set; } = () => { };

        public void Play(ReadOnlyCollection<string> actionList)
        {
            StartCoroutine(RunEnumerator(actionList));
        }

        private IEnumerator RunEnumerator(ReadOnlyCollection<string> actionList)
        {
            actions.Clear();
            foreach (string action in actionList)
                actions.Enqueue(action);

            onStart.Invoke();
            Time.timeScale = 0f;
            promptPanel.gameObject.SetActive(true); 
            while (actions.Count > 0 || waitingForInput == true)
            {
                if (waitingForInput == false && actionList.Count > 0)
                {
                    //set menu picture
                    promptText.text = actions.Peek().Split(':')[2];
                    actions.Dequeue();
                    yield return TypeWriterTextUpdate();
                    waitingForInput = true;
                }
                yield return new WaitForSecondsRealtime(0.02f);
            }
            Time.timeScale = 1f;

            onStop.Invoke();
            promptPanel.gameObject.SetActive(false);
        }

        private IEnumerator TypeWriterTextUpdate()
        {
            int count = promptText.text.Length;
            promptText.maxVisibleCharacters = 0;

            for (int i = 0; i < count; i++)
            {
                promptText.maxVisibleCharacters++;
                yield return new WaitForSecondsRealtime(0.02f);
            }
        }
    }
}