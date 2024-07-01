using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.DontDestroyOnLoad
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractableBase : MonoBehaviour
    {
        [SerializeField] protected string id = "None";

        protected string getID => id;
        public virtual string getAction => "Interact with ";

        protected virtual void OnValidate()
        {
            if (id == "None")
                id = System.DateTime.Now.Ticks.ToString() + "|" + System.Guid.NewGuid().ToString();    
        }

        protected void Start()
        {
            
        }

        public virtual void Interact(IActor player)
        {

        }

        public virtual void ActivateScriptedSequence (ScriptedSequencerActionSO action)
        {
            Transform scriptedSequencerDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/ScriptedSequencerDisplay").transform;

            IScriptedSequencerData.actions.Clear();
            IScriptedSequencerData.actions.Enqueue(action);

            scriptedSequencerDisplay.gameObject.SetActive(true);
        }
    }
}