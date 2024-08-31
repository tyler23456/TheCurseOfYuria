using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class TransformTeleporter : InteractableBase, IInteractableTrigger
    {
        [SerializeField] Transform destination;
        [SerializeField] ITransformTeleporter.State state;

        public override string getAction => "";

        public override void Interact(IActor player)
        {
            base.Interact(player);

            switch (state)
            {
                case ITransformTeleporter.State.arbitrary:
                    TransitionToArbitrary();
                    break;
                case ITransformTeleporter.State.Interior:
                    TransitionToInterior();
                    break;
                case ITransformTeleporter.State.Exterior:
                    TransitionToExterior();
                    break;
            }

            SetPositionOfAllies(player, destination.position, new Vector3(0f, destination.eulerAngles.y, 0f));

            Transform allies = player.obj.transform.parent;

            allies.GetComponent<IPlayerControls>().Refresh();

            ITransformTeleporter.state = state;
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