using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TCOY.Canvas
{
    public class CutsceneDisplay : MonoBehaviour
    {
        IGlobal global;
        IFactory factory;

        [SerializeField] Image promptImage;
        [SerializeField] TMP_Text promptText;


        void OnEnable()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
            IGlobal.gameState = IGlobal.GameState.Stopped;
            //need to calculate the actors in the field 
            global.StartCoroutine(Activate(global, factory, new List<IActor>()));
        }

        void OnDisable()
        {
            IGlobal.gameState = IGlobal.GameState.Stopped;
        }

        public IEnumerator Activate(IGlobal global, IFactory factory, List<IActor> actors)
        {
            foreach (ActionBase action in global.cutsceneActions)
                yield return action.Activate(global, factory, actors);

            gameObject.SetActive(false);
        }

        
    }
}