using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Climber : InteractableBase, IInteractableTrigger, IClimber
    {
        static IController controller;

        public override void Interact(IActor player)
        {
            base.Interact(player);

            controller = player.obj.GetComponent<IController>();

            if (controller.action.name != "ClimbState")
                return;

            controller.SetAction(StateDatabase.Instance.GetAction("ClimbState"));
        }
    }
}