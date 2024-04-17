using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

namespace TCOY.Interactables
{
    public class Cutscene : InteractableBase, IInteractable
    {
        //Sarah:Prompt:Hey everybody.  How's it going today
        //Rowin:walk:Left:2
        //Rowin:Sprint:Right:0.5
        //Camera:Pan:Right:2

        [SerializeField]
        [TextArea(3, 10)]
        List<string> actions;

        public override void Interact(IPlayer player)
        {
            base.Interact(player);

            cutscene.Play(actions.AsReadOnly());
        }

    }
}