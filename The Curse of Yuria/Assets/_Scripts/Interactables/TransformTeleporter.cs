using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class TransformTeleporter : InteractableBase, IInteractableTrigger
    {
        enum State { arbitrary, Interior, Exterior }

        [SerializeField] Transform destination;
        [SerializeField] State state;

        public override string getAction => "";

        public override void Interact(IActor player)
        {
            base.Interact(player);

            switch (state)
            {
                case State.arbitrary:
                    TransitionToArbitrary();
                    break;
                case State.Interior:
                    TransitionToInterior();
                    break;
                case State.Exterior:
                    TransitionToExterior();
                    break;
            }

            SetPositionOfAllies(player, destination.position, new Vector3(0f, destination.eulerAngles.y, 0f));

            Transform allies = player.obj.transform.parent;

            int count = Mathf.Min(allies.childCount, IAllie.MaxActiveAlliesCount);

            for (int i = 1; i < count; i++)
                if (allies.childCount >= 3)
                {
                    IController controller = allies.GetChild(i).GetComponent<IController>();
                    controller.ResetToDefault();
                }
        }



        void TransitionToArbitrary()
        {

        }

        void TransitionToInterior()
        {
            foreach (Transform child in destination.parent.parent)
                if (child == destination.parent)
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);

            IVolume.interiorAmbience = 1f;
            IVolume.exteriorAmbience = 0f;
        }

        void TransitionToExterior()
        {
            IVolume.interiorAmbience = 0f;
            IVolume.exteriorAmbience = 1f;
        }
    }
}