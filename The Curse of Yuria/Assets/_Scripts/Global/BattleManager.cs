using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] TargeterBase enemyTargeter;
        [SerializeField] List<StatusEffectBase> gameOverStatusEffects;

        public void Start()
        {
            Global.Instance.StartCoroutine(BattleSystemLoop());
        }

        void Update()
        {
            CheckForGameOver();
        }

        IEnumerator BattleSystemLoop()
        {
            while (true)
            {
                if (Global.Instance.gameState != Global.GameState.Playing)
                    yield return new WaitForEndOfFrame();

                yield return new WaitForEndOfFrame();

                RefreshNearbyEnemies();

                CheckForCounters();

                CheckForInterrupts();

                if (Global.Instance.pendingCommands.Count == 0)
                    continue;     

                Command command = Global.Instance.pendingCommands.First();
                Global.Instance.pendingCommands.RemoveFirst();

                if (command.user == null)
                    continue;

                command.user.StartCoroutine(command.item.Use(command.user, command.targets));
                Global.Instance.successfulCommands.AddLast(command);
                yield return new WaitForSeconds(1f);
            }
        }

        void CheckForCounters()
        {
            if (Global.Instance.successfulCommands.Count == 0 || !Global.Instance.successfulCommands.Last().isCounterable)
                return;

            List<Command> counters = Global.Instance.allies.CalculateCounters(Global.Instance.successfulCommands.Last());
            List<Command> counters2 = Global.Instance.enemies.CalculateCounters(Global.Instance.successfulCommands.Last());
            counters.AddRange(counters2);

            Global.Instance.successfulCommands.Last().isCounterable = false; 

            foreach (Command command in counters)
            {
                command.isCounterable = false;
                Global.Instance.pendingCommands.AddLast(command);
            }
        }

        void CheckForInterrupts()
        {
            if (Global.Instance.pendingCommands.Count == 0 || !Global.Instance.pendingCommands.First().isInterruptable)
                return;

            List<Command> interrupts = Global.Instance.allies.CalculateInterrupts(Global.Instance.pendingCommands.First());
            List<Command> interrupts2 = Global.Instance.enemies.CalculateInterrupts(Global.Instance.pendingCommands.First());
            interrupts.AddRange(interrupts2);

            Global.Instance.pendingCommands.First().isInterruptable = false;

            foreach (Command command in interrupts)
            {
                command.isInterruptable = false;
                Global.Instance.pendingCommands.AddFirst(command);
            }
        }

        void RefreshNearbyEnemies()
        {
            List<IActor> targets = enemyTargeter.CalculateTargets(Global.Instance.allies.GetPositionAt(0));
            Global.Instance.enemies.Set(targets);
        }

        void CheckForGameOver()
        {
            if (Global.Instance.gameState != Global.GameState.Playing)
                return;

            if (Global.Instance.allies.AllContainAnyOf(gameOverStatusEffects))
                GameOverDisplay.Instance.ShowExclusivelyInParent();
        }
    }
}