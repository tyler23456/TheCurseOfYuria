using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Transform aTBGuagesFilled;
    [SerializeField] Transform pendingCommands;
    [SerializeField] Transform executingCommands;
    [SerializeField] Transform successfulCommands;
    [SerializeField] Transform allies;
    [SerializeField] Transform enemies;
    [SerializeField] Transform gameOverDisplay;
    
    [SerializeField] TrajectoryPathDrawer lineDrawerPrefab;
    [SerializeField] TargeterBase enemyTargeter;
    [SerializeField] List<StatusEffectBase> gameOverStatusEffects;

    public int aTBGuageFilledCount => aTBGuagesFilled.childCount;
    public Command previousSuccessfulCommand => successfulCommands.GetChild(successfulCommands.childCount - 1).GetComponent<Command>();
    public Command nextPendingCommand => pendingCommands.GetChild(0).GetComponent<Command>();

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
            DestroyBrokenExecutingCommands();

            if (!GameStateManager.Instance.isPlaying)
                yield return new WaitForEndOfFrame();

            yield return new WaitForEndOfFrame();

            RefreshNearbyEnemies();

            CheckForCounters();

            CheckForInterrupts();

            if (pendingCommands.childCount == 0)
                continue;

            Command command = nextPendingCommand;
            command.transform.parent = executingCommands;

            TrajectoryPathDrawer drawer = Instantiate(lineDrawerPrefab.gameObject).GetComponent<TrajectoryPathDrawer>();
            drawer.onFinishedDrawing = () => RunCommand(command);
            drawer.Initialize(command.user.obj.transform, command.targets[0].obj.transform, command.user.trajectoryPathColor);

            yield return new WaitForSeconds(1f);
        }
    }

    void DestroyBrokenExecutingCommands()
    {
        foreach (Transform child in executingCommands)
            if (child.GetComponent<Command>() == null)
                Destroy(child.gameObject);
    }

    void RunCommand(Command command)
    {
        if (command == null)
            return;

        if (command.user == null || !command.user.getATBGuage.isActive)
        {
            Destroy(command.gameObject);
            return;
        }

        command.user.StartCoroutine(command.item.Use(command.user, command.targets));
        command.transform.parent = successfulCommands;
    }

    void CheckForCounters()
    {
        if (successfulCommands.childCount == 0 || !previousSuccessfulCommand.isCounterable)
            return;

        Command previousCommand = previousSuccessfulCommand;

        CalculateReactors(previousCommand, allies, true);
        CalculateReactors(previousCommand, enemies, true);

        previousCommand.isCounterable = false;
    }

    void CheckForInterrupts()
    {
        if (pendingCommands.childCount == 0 || !nextPendingCommand.isInterruptable)
            return;

        Command nextCommand = nextPendingCommand;

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
                if (((1 << command.targets[0].obj.layer) & reactor.getMask) != 0 && command.item.name == reactor.getItemName)
                {
                    Command reaction = new GameObject("Command").AddComponent<Command>();
                    reaction.Set(actor, reactor.getReaction, reactor.getTargeter.CalculateTargets(actor.obj.transform.position));
                    reaction.transform.parent = pendingCommands;

                    if (isCounter)
                    {
                        command.isCounterable = false;
                    }
                    else
                    {
                        command.isInterruptable = false;
                        reaction.transform.SetSiblingIndex(0);
                    }
                }
        }
    }

    void RefreshNearbyEnemies()
    {
        IActor[] targets = enemyTargeter.CalculateTargets(allies.GetChild(0).position); //calculate targets from where

        foreach (Transform t in enemies)
            t.parent = null;

        foreach (IActor actor in targets)
            actor.obj.transform.parent = enemies;
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
