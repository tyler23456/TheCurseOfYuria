using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    public class Player : Actor, IPlayer
    {

        new protected void Start()
        {
            base.Start();

            aTBGuage.OnATBGuageFilled = () => global.aTBGuageFilledQueue.Enqueue(this);
            aTBGuage.OnATBGuageFilled += () => Debug.Log("Filled");
            
        }

        new protected void Update()
        {
            base.Update();


        }
    }
}