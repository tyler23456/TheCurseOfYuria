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
            Global.instance.StartCoroutine(BattleSystemLoop());
        }

        void Update()
        {
            CheckForGameOver();
        }

        IEnumerator BattleSystemLoop()
        {
            while (true)
            {
                if (Global.instance.gameState != Global.GameState.Playing)
                    yield return new WaitForEndOfFrame();

                yield return new WaitForEndOfFrame();

                RefreshNearbyEnemies();

                CheckForCounters();

                CheckForInterrupts();

                if (Global.instance.pendingCommands.Count == 0)
                    continue;     

                Command command = Global.instance.pendingCommands.First();
                Global.instance.pendingCommands.RemoveFirst();

                if (command.user == null)
                    continue;

                command.user.StartCoroutine(command.item.Use(command.user, command.targets));
                Global.instance.successfulCommands.AddLast(command);
                yield return new WaitForSeconds(1f);
            }
        }

        void CheckForCounters()
        {
            if (Global.instance.successfulCommands.Count == 0 || !Global.instance.successfulCommands.Last().isCounterable)
                return;

            List<Command> counters = Global.instance.allies.CalculateCounters(Global.instance.successfulCommands.Last());
            List<Command> counters2 = Global.instance.enemies.CalculateCounters(Global.instance.successfulCommands.Last());
            counters.AddRange(counters2);

            Global.instance.successfulCommands.Last().isCounterable = false; 

            foreach (Command command in counters)
            {
                command.isCounterable = false;
                Global.instance.pendingCommands.AddLast(command);
            }
        }

        void CheckForInterrupts()
        {
            if (Global.instance.pendingCommands.Count == 0 || !Global.instance.pendingCommands.First().isInterruptable)
                return;

            List<Command> interrupts = Global.instance.allies.CalculateInterrupts(Global.instance.pendingCommands.First());
            List<Command> interrupts2 = Global.instance.enemies.CalculateInterrupts(Global.instance.pendingCommands.First());
            interrupts.AddRange(interrupts2);

            Global.instance.pendingCommands.First().isInterruptable = false;

            foreach (Command command in interrupts)
            {
                command.isInterruptable = false;
                Global.instance.pendingCommands.AddFirst(command);
            }
        }

        void RefreshNearbyEnemies()
        {
            List<IActor> targets = enemyTargeter.CalculateTargets(Global.instance.allies.GetPositionAt(0));
            Global.instance.enemies.Set(targets);
        }

        void CheckForGameOver()
        {
            if (Global.instance.gameState != Global.GameState.Playing)
                return;

            if (Global.instance.allies.AllContainAnyOf(gameOverStatusEffects))
                Global.instance.ToggleDisplay(Global.Display.GameOver);
        }
    }
}