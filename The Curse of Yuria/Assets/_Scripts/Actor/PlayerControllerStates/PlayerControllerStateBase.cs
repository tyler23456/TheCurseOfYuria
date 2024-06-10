using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.PlayerControls
{
    public class PlayerControllerStateBase
    {
        bool isEntering = true;

        public void Update(PlayerControls controls)
        {
            if (isEntering)
            {
                Enter(controls);
                isEntering = false;
            } 

            Stay(controls);
        }

        public void MarkStateAsFinished(PlayerControls controls)
        {
            Exit(controls);
            isEntering = true;
        }

        protected virtual void Enter(PlayerControls controls) { }
        protected virtual void Stay(PlayerControls controls) { }
        protected virtual void Exit(PlayerControls controls) { }
    }
}