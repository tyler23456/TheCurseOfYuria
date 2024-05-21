using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.DontDestroyOnLoad
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] protected string id = "None";

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
        }

        void OnValidate()
        {
            if (id == "None")
                id = System.DateTime.Now.Ticks.ToString() + "|" + System.Guid.NewGuid().ToString();    
        }

        public virtual void Interact(IAllie player)
        {
        }

        private void OnTriggerStay2D(Collider2D collision)
        {   
            IPlayerControls playerControls = collision.GetComponent<IPlayerControls>();

            if (playerControls == null || !Input.GetKeyDown(KeyCode.Mouse0))
                return;

            IAllie player = collision.GetComponent<IAllie>();

            Interact(player);
        }
    }
}