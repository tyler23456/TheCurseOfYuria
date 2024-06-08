using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class Hittable : Actor
    {

        protected new void Awake()
        {
            base.Awake();

            stats.onZeroHealth += () => gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}