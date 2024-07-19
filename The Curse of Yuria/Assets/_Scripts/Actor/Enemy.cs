using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Enemy : Actor, IEnemy, IActor
    {
        [SerializeField] List<Move> moves;

        Queue<Move> movesQueue = new Queue<Move>();

        public new Rigidbody2D rigidbody2D { get; private set; }
        public Animator animator { get; private set; }
        public List<Move> getMoves => moves;

        protected new void Awake()
        {
            base.Awake();

            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            aTBGuage.OnATBGuageFilled = MakeADecision;
            aTBGuage.OnATBGuageFilled += () => aTBGuage.Reset();

            stats.onZeroHealth += () => GetComponent<IEnabler>().enabled = true;

            foreach (Move move in moves)
                movesQueue.Enqueue(move);
        }

        protected new void Update()
        {
            base.Update();

            

        }

        void MakeADecision()
        {
            if (TargeterDatabase.Instance.getNearbyAllieTargeter.CalculateTargets(collider2D.bounds.center).Length == 0)
                return;

            IActor[] targets = movesQueue.Peek().targeter.CalculateTargets(collider2D.bounds.center);

            if (targets.Length == 0)
            {
                aTBGuage.Reset();
                movesQueue.Enqueue(movesQueue.Dequeue());
                return;
            }

            Command command = new Command(this, movesQueue.Peek().skill, targets);
            IBattleData.pendingCommands.AddLast(command);
            movesQueue.Enqueue(movesQueue.Dequeue());
        }
    }
}