using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TCOY.Canvas
{
    public class CutsceneDisplay : MonoBehaviour
    {
        [SerializeField] Image promptImage;
        [SerializeField] TMP_Text promptText;


        void OnEnable()
        {
            Global.instance.gameState = Global.GameState.Stopped;
            //need to calculate the actors in the field 
            Global.instance.StartCoroutine(Activate(new List<IActor>()));
        }

        void OnDisable()
        {
            Global.instance.gameState = Global.GameState.Playing;
        }

        public IEnumerator Activate(List<IActor> actors)
        {
            while (Global.instance.cutsceneActions.Count > 0)
                yield return Global.instance.cutsceneActions.Dequeue().Activate(actors);

            gameObject.SetActive(false);
        }
    }
}