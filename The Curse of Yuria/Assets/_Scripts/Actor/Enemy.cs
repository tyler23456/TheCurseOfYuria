using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class Enemy : Actor, IEnemy
    {
        [SerializeField] float proximity = 5f;
        [SerializeField] List<Move> moves;

        Queue<Move> movesQueue = new Queue<Move>();

        protected new void Start()
        {
            base.Start();

            aTBGuage.OnATBGuageFilled = MakeADecision;
            aTBGuage.OnATBGuageFilled += () => aTBGuage.Reset();

            foreach (Move move in moves)
                movesQueue.Enqueue(move);
        }

        protected new void Update()
        {
            base.Update();

            

        }

        void MakeADecision()
        {
            IActor target = Global.instance.allies[0];
            if (Vector3.Distance(transform.position, target.getGameObject.transform.position) > proximity)
                return;

            List<IActor> targets = movesQueue.Peek().getTargeter.CalculateTargets(transform.position);

            if (targets.Count == 0)
            {
                aTBGuage.Reset();
                movesQueue.Enqueue(movesQueue.Dequeue());
                return;
            }


            Global.instance.pendingCommands.AddLast(new Command(this, movesQueue.Peek().getskill, targets));
            movesQueue.Enqueue(movesQueue.Dequeue());
        }
    }
}