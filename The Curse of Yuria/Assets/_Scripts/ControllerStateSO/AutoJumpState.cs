using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoJumpState", menuName = "ControllerStates/AutoJumpState")]
    public class AutoJumpState : StateBase
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