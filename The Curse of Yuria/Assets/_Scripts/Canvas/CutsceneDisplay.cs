using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TCOY.Canvas
{
    public class CutsceneDisplay : MenuBase
    {
        [SerializeField] Image promptImage;
        [SerializeField] TMP_Text promptText;

        protected new void OnEnable()
        {
            base.OnEnable();

            Global.instance.gameState = Global.GameState.Stopped; 
            Global.instance.StartCoroutine(Activate(new List<IActor>()));
        }

        protected new void OnDisable()
        {
            base.OnDisable();
        }

        public IEnumerator Activate(List<IActor> actors)
        {
            while (Global.instance.cutsceneActions.Count > 0)
                yield return Global.instance.cutsceneActions.Dequeue().Activate(actors);

            gameObject.SetActive(false);
        }
    }
}