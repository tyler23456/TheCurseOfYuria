using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class Detection : IDetection
    {
        int priority = 0;

        public int getPriority => priority;

        public void RaisePriority()
        {
            priority++;

            if (priority > 0)
                priority = 0;
        }

        public void LowerPriority()
        {
            priority--;
        }
    }
}