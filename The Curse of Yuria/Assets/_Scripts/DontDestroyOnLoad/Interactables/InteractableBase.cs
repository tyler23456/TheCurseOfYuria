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

        protected string getID => id;

        void OnValidate()
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

        private void OnTriggerStay2D(Collider2D collision)
        {   
            IPlayerControls playerControls = collision.GetComponent<IPlayerControls>();

            if (playerControls == null || !Input.GetKeyDown(KeyCode.Mouse0))
                return;

            IActor player = collision.GetComponent<IAllie>();

            Interact(player);
        }
    }
}