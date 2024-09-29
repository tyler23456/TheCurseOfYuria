using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TCOY.Interactables
{
    public class TransformTeleporter : InteractableBase, IInteractableTrigger
    {
        [SerializeField] Transform destination;
        [SerializeField] ITransformTeleporter.State state;

        static string parentName = "Interiror";
        static int childIndex = 0;

        public override string getAction => "";

        public override void Interact(IActor player)
        {
            base.Interact(player);

            parentName = destination.parent.parent.name;
            childIndex = destination.parent.GetSiblingIndex();

            SaveManager.instance.ClearNonPersistentSceneData();
            SetPositionOfAllies(player, destination.position, new Vector3(0f, destination.eulerAngles.y + 180f, 0f));

            Transform loadingDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/LoadingDisplay").transform;

            ILoadingData.sceneID = SceneManager.GetActiveScene().buildIndex;
            ILoadingData.onFinishedLoading = () => OnFinishedLoading(player);
            loadingDisplay.gameObject.SetActive(true);
        }

        void OnFinishedLoading(IActor player)
        {
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

            Transform allies = player.obj.transform.parent;

            allies.GetComponent<IPlayerControls>().Refresh();

            ITransformTeleporter.state = state;
        }



        void TransitionToArbitrary()
        {

        }

        void TransitionToInterior()
        {
            Transform parent = GameObject.Find("/" + parentName).transform;

            foreach (Transform child in parent)
                if (child.GetSiblingIndex() == childIndex)
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