using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoJumpState", menuName = "AutoActionStates/AutoJumpState")]
    public class AutoJumpState : ActionBase
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            /*Waypoint waypoint = collision.GetComponent<Waypoint>();

            if (waypoint == null)
                return;

            //need really good jump checker
            velocity += Vector2.up * 100f;*/
        }
    }
}