using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "BattleState", menuName = "GoalStates/BattleState")]
    public class BattleState : GoalBase
    {
        protected override void Enter(IController controller)
        {
            if (controller.actor.obj.transform.parent.name == "Allies")
                controller.target = controller.actor.obj.transform.parent.GetChild(controller.actor.obj.transform.GetSiblingIndex() - 1).GetComponent<IPath>();            
            else
                controller.target = GameObject.Find("/DontDestroyOnLoad/Allies").transform.GetChild(0).GetComponent<IPath>();

            controller.SetAction(StateDatabase.Instance.GetAction("AutoResetState"));
        
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);
        }

        protected override void Exit(IController controller)
        {
        }
    }
}
