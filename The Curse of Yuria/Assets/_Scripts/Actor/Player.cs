using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class Player : Actor, IAllie
    {

        new protected void Start()
        {
            base.Start();

            aTBGuage.OnATBGuageFilled = () => Global.instance.aTBGuageFilledQueue.Enqueue(this);
            aTBGuage.OnATBGuageFilled += () => Debug.Log("Filled");  
        }

        new protected void Update()
        {
            base.Update();


        }
    }
}