using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    public class Enemy : Actor, IEnemy
    {
        [SerializeField] List<Move> moves;

        Queue<Move> movesQueue = new Queue<Move>();

        protected new void Start()
        {
            base.Start();

            aTBGuage.OnATBGuageFilled = MakeADecision;

            foreach (Move move in moves)
                movesQueue.Enqueue(move);
        }

        protected new void Update()
        {
            base.Update();

            

        }

        void MakeADecision()
        {
            List<IActor> targets = movesQueue.Peek().getTargeter.CalculateTargets(transform.position);

            if (targets == null)
            {
                aTBGuage.Reset();
                movesQueue.Enqueue(movesQueue.Dequeue());
                return;
            }


            global.pendingCommands.Enqueue(new Command(this, movesQueue.Peek().getskill, targets.ToArray()));

            aTBGuage.Reset();
            movesQueue.Enqueue(movesQueue.Dequeue());
        }
    }
}