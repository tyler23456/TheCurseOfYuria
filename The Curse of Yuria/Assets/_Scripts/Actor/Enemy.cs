using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    public class Enemy : Actor, IEnemy
    {
        [SerializeField] List<Move> moves;


        protected new void Start()
        {
            base.Start();

            aTBGuage.OnATBGuageFilled = MakeADecision;
        }

        protected new void Update()
        {
            base.Update();

            

        }

        void MakeADecision()
        {
            List<IActor> targets = moves[0].getTargeter.CalculateTargets(transform.position);

            if (targets == null)
            {
                aTBGuage.Reset();
                return;
            }


            global.pendingCommands.Enqueue(new Command(this, moves[0].getskill, targets.ToArray()));

            aTBGuage.Reset();
        }
    }
}