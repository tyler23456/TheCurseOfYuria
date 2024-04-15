using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.Interactables
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] List<string> completedQuests;
        [SerializeField] List<string> questItems;

        protected IGlobal global;

        public void Start()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

            if (questItems.Count == 0 || questItems.TrueForAll(i => global.getQuestItems.Contains(i)))
                Destroy(gameObject);

            if (completedQuests.Count == 0 || completedQuests.TrueForAll(i => global.getCompletedQuests.Contains(i)))
                Destroy(gameObject);
        }

        public virtual void Interact(IPlayer player)
        {
            
        }

        protected void OnTriggerStay(Collider other)
        {
            IPlayerControls playerControls = other.GetComponent<IPlayerControls>();

            if (playerControls == null)
                return;

            IPlayer player = other.GetComponent<IPlayer>();

            Interact(player);
        }
    }
}