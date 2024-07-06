using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractableBase : MonoBehaviour
    {
        [SerializeField] protected string ID = "";

        protected string getID => ID;
        public virtual string getAction => "Interact with ";

        protected virtual void OnValidate()
        {
            if (ID == "")
                ID = System.DateTime.Now.Ticks.ToString() + "|" + System.Guid.NewGuid().ToString();    
        }

        protected void Start()
        {
            
        }

        public virtual void Interact(IActor player)
        {

        }

        public virtual void ActivateScriptedSequence (IScriptedSequencerAction action)
        {
            Transform scriptedSequencerDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/ScriptedSequencerDisplay").transform;

            IScriptedSequencerData.actions.Clear();
            IScriptedSequencerData.actions.Enqueue(action);

            scriptedSequencerDisplay.gameObject.SetActive(true);
        }
    }
}