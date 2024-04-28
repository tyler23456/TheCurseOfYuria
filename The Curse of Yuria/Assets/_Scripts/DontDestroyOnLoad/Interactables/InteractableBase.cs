using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.DontDestroyOnLoad
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class InteractableBase : MonoBehaviour
    {
        string id;

        GameObject obj;
        protected IGlobal global;
        protected IFactory factory;
        protected ICutscene cutscene;

        protected string getID => id;

        public void Start()
        {
            obj = GameObject.Find("/DontDestroyOnLoad");
            global = obj.GetComponent<IGlobal>();
            factory = obj.GetComponent<IFactory>();
            cutscene = obj.GetComponent<ICutscene>();

            /*if (minimumCompletedQuests.TrueForAll(i => global.getCompletedQuests.Contains(i)) && minimumQuestItems.TrueForAll(i => global.getQuestItems.Contains(i)))
                isUnlocked = true;

            if (maximumCompletedQuests.TrueForAll(i => global.getCompletedQuests.Contains(i)) && maximumQuestItems.TrueForAll(i => global.getQuestItems.Contains(i)))
                isUnlocked = false;

            if (isUnlocked)
                Unlock();*/
        }

        public virtual void Interact(IPlayer player)
        {
            
        }

        public virtual void Use(IActor user, IActor[] targets)
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