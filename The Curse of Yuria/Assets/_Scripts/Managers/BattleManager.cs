using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Transform allies;
    [SerializeField] Transform enemies;
    [SerializeField] Transform gameOverDisplay;
    
    [SerializeField] TrajectoryPathDrawer lineDrawerPrefab;
    [SerializeField] TargeterBase enemyTargeter;
    [SerializeField] List<StatusEffectBase> gameOverStatusEffects;


    public void Start()
    {
        StartCoroutine(BattleSystemLoop());
    }

    void Update()
    {
        CheckForGameOver();
    }

    IEnumerator BattleSystemLoop()
    {
        while (true)
        {
            if (!GameStateManager.Instance.isPlaying)
                yield return new WaitForEndOfFrame();

            yield return new WaitForEndOfFrame();

            RefreshNearbyEnemies();

            CheckForCounters();

            CheckForInterrupts();

            if (IBattleData.pendingCommands.Count == 0)
                continue;

            Command command = IBattleData.pendingCommands.First.Value;
            IBattleData.pendingCommands.RemoveFirst();

            TrajectoryPathDrawer drawer = Instantiate(lineDrawerPrefab.gameObject).GetComponent<TrajectoryPathDrawer>();
            drawer.onFinishedDrawing = () => RunCommand(command);
            drawer.Initialize(command.user.getCollider2D, command.targets[0].getCollider2D, command.user.trajectoryPathColor);

            yield return new WaitForSeconds(1f);
        }
    }

    void RunCommand(Command command)
    {
        if (command == null || command.user == null || !command.user.getATBGuage.isActive)
            return;

        command.user.StartCoroutine(command.item.Use(command.user, command.targets));
        IBattleData.successfulCommands.AddLast(command);
    }

    void CheckForCounters()
    {
        if (IBattleData.successfulCommands.Count == 0 || !IBattleData.successfulCommands.Last.Value.isCounterable)
            return;

        Command previousCommand = IBattleData.successfulCommands.Last.Value;

        CalculateReactors(previousCommand, allies, true);
        CalculateReactors(previousCommand, enemies, true);

        previousCommand.isCounterable = false;
    }

    void CheckForInterrupts()
    {
        if (IBattleData.pendingCommands.Count == 0 || !IBattleData.pendingCommands.First.Value.isInterruptable)
            return;

        Command nextCommand = IBattleData.pendingCommands.First.Value;

        CalculateReactors(nextCommand, allies, false);
        CalculateReactors(nextCommand, enemies, false);

        nextCommand.isInterruptable = false;
    }

    public void CalculateReactors(Command command, Transform actorsParent, bool isCounter)
    {
        IActor actor = null;

        foreach (Transform t in actorsParent)
        {
            actor = t.GetComponent<IActor>();
            List<Reactor> reactors = isCounter ? actor.getCounters : actor.getInterrupts;

            foreach (Reactor reactor in reactors)
                if (((1 << command.targets[0].obj.layer) & reactor.mask) != 0 && command.item.ContainsType(reactor.type.name))
                {
                    Command reaction = new Command(actor, reactor.reaction, reactor.target.CalculateTargets(actor.getCollider2D.bounds.center));

                    if (isCounter)
                        IBattleData.pendingCommands.AddLast(reaction);
                    else
                        IBattleData.pendingCommands.AddFirst(reaction);
                }
        }
    }

    void RefreshNearbyEnemies()
    {
        IActor[] enemyTargets = enemyTargeter.CalculateTargets(allies.GetChild(0).GetComponent<IActor>().getCollider2D.bounds.center);

        foreach (Transform t in enemies)
            t.parent = null;

        foreach (IActor enemyTarget in enemyTargets)
            enemyTarget.obj.transform.parent = enemies;
    }

    void CheckForGameOver()
    {
        if (!GameStateManager.Instance.isPlaying)
            return;

        int count = Mathf.Min(allies.childCount, IAllie.MaxActiveAlliesCount);

        for (int i = 0; i < count; i++)
            if (gameOverStatusEffects.All(statusEffect => !allies.GetChild(i).GetComponent<IActor>().getStatusEffects.Contains(statusEffect.name)))
                return;

        gameOverDisplay.gameObject.SetActive(true);
    }

   
}
