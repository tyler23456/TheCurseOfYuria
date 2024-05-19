using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        IGlobal global;

        [SerializeField] TargeterBase enemyTargeter;
        [SerializeField] List<StatusEffectBase> gameOverStatusEffects;

        public void Start()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            global.StartCoroutine(BattleSystemLoop());
        }

        void Update()
        {
            CheckForGameOver();
        }

        IEnumerator BattleSystemLoop()
        {
            while (true)
            {
                if (IGlobal.gameState != IGlobal.GameState.Playing)
                    yield return new WaitForEndOfFrame();

                yield return new WaitForEndOfFrame();

                RefreshNearbyEnemies();

                CheckForCounters();

                CheckForInterrupts();

                if (global.pendingCommands.Count == 0)
                    continue;

                Command command = global.pendingCommands.First();
                global.pendingCommands.RemoveFirst();

                if (command.user == null)
                    continue;

                command.user.StartCoroutine(command.item.Use(command.user, command.targets));
                yield return new WaitForSeconds(1f);
            }
        }

        void CheckForCounters()
        {
            if (global.successfulCommands.Count == 0)
                return;

            List<Command> counters = global.allies.CalculateCounters(global.successfulCommands.Last());
            List<Command> counters2 = global.enemies.CalculateCounters(global.successfulCommands.Last());
            counters.AddRange(counters2);
            foreach (Command command in counters)
                global.pendingCommands.AddLast(command);
        }

        void CheckForInterrupts()
        {
            if (global.pendingCommands.Count == 0)
                return;

            List<Command> counters = global.allies.CalculateCounters(global.pendingCommands.First());
            List<Command> counters2 = global.enemies.CalculateCounters(global.pendingCommands.First());
            counters.AddRange(counters2);
            foreach (Command command in counters)
                global.pendingCommands.AddFirst(command);
        }

        void RefreshNearbyEnemies()
        {
            List<IActor> targets = enemyTargeter.CalculateTargets(global.allies.GetPositionAt(0));
            global.enemies.Set(targets);
        }

        void CheckForGameOver()
        {
            if (IGlobal.gameState != IGlobal.GameState.Playing)
                return;

            if (global.allies.AllContainAnyOf(gameOverStatusEffects))
                global.ToggleDisplay(IGlobal.Display.GameOverDisplay);
        }
    }
}