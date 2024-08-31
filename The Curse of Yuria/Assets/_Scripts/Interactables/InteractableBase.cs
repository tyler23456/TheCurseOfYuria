using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractableBase : MonoBehaviour
    {
        static bool isFirst = true;
        protected static new Camera camera;

        public virtual string getAction => "Interact with ";

        public void Awake()
        {
            if (!isFirst)
                return;

            isFirst = false;

            camera = GameObject.Find("/DontDestroyOnLoad/Main Camera").GetComponent<Camera>();
        }

        public virtual void Interact(IActor player)
        {

        }

        public virtual bool CannotShowActionText(IActor player)
        {
            return false;
        }

        public virtual void ActivateScriptedSequence (ActionSO action)
        {
            Transform scriptedSequencerDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/ScriptedSequencerDisplay").transform;

            IScriptedSequencerData.actions.Clear();
            IScriptedSequencerData.actions.Enqueue(action);

            scriptedSequencerDisplay.gameObject.SetActive(true);
        }

        protected virtual void SetPositionOfAllies(IActor player, Vector2 position, Vector3 eulerAngles)
        {
            bool previousKinematic = false;
            IController controller = null;
            foreach (Transform allie in player.obj.transform.parent)
            {
                controller = allie.GetComponent<IController>();
                previousKinematic = controller.rigidbody2D.isKinematic;
                controller.rigidbody2D.isKinematic = true;
                allie.position = position;
                allie.eulerAngles = eulerAngles;
                controller.rigidbody2D.isKinematic = previousKinematic;
                controller.rigidbody2D.Sleep();
                controller.rigidbody2D.WakeUp();
            }

            camera.transform.position = player.obj.transform.position + new Vector3(0f, 0f, -1f);
        }
    }
}