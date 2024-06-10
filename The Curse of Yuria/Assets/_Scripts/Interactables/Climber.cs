using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Climber : InteractableBase, IInteractableTrigger, IClimber
    {
        
        public override void Interact(IActor player)
        {
            base.Interact(player);

            if (IPlayerControls.state != IPlayerControls.State.Normal)
                return;

            IPlayerControls.state = IPlayerControls.State.Climb;
        }
    }
}