using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Enemy : Actor, IEnemy, IActor
    {
        [SerializeField] TargeterBase targeterForHostility;
        [SerializeField] List<Move> moves;

        Queue<Move> movesQueue = new Queue<Move>();

        public new Rigidbody2D rigidbody2D { get; private set; }
        public Animator animator { get; private set; }  

        protected new void Awake()
        {
            base.Awake();

            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            aTBGuage.OnATBGuageFilled = MakeADecision;
            aTBGuage.OnATBGuageFilled += () => aTBGuage.Reset();

            stats.onZeroHealth += () => GetComponent<DontDestroyOnLoad.RandomDrop>().enabled = true;

            foreach (Move move in moves)
                movesQueue.Enqueue(move);
        }

        protected new void Update()
        {
            base.Update();

            

        }

        void MakeADecision()
        {
            if (targeterForHostility.CalculateTargets(transform.position).Count == 0)
                return;

            List<IActor> targets = movesQueue.Peek().getTargeter.CalculateTargets(transform.position);

            if (targets.Count == 0)
            {
                aTBGuage.Reset();
                movesQueue.Enqueue(movesQueue.Dequeue());
                return;
            }


            BattleManager.Instance.AddCommand(new Command(this, movesQueue.Peek().getskill, targets));
            movesQueue.Enqueue(movesQueue.Dequeue());
        }
    }
}