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
    [SerializeField] Targeter closeEnemyTargeter;
    [SerializeField] Targeter farEnemyTargeter;
    [SerializeField] List<StatusEffectBase> gameOverStatusEffects;

    HashSet<IActor> closeEnemies = new HashSet<IActor>();
    HashSet<IActor> farEnemies = new HashSet<IActor>();
    HashSet<IActor> enemyTargets = new HashSet<IActor>();
    HashSet<IActor> enemiesToRemove = new HashSet<IActor>();

    List<Command> allieCommands = new List<Command>();
    List<Command> commandsToRemove = new List<Command>();

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

            RemoveBrokenCommands();

            RefreshNearbyEnemies();

            CheckForCounters();

            CheckForInterrupts();

            if (IBattleData.pendingCommands.Count == 0)
                continue;

            Command command = IBattleData.pendingCommands.First.Value;
            IBattleData.pendingCommands.RemoveFirst();

            command.user.RotateToward(command.user.getCollider2D.bounds.center);

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

    void RemoveBrokenCommands()
    {
        commandsToRemove.Clear();

        foreach (Command command in IBattleData.pendingCommands)
        {
            if (command.user == null || !command.user.getATBGuage.isActive)
            {
                commandsToRemove.Add(command);
                continue;
            }
            command.targets.RemoveAll(i => i == null);
        }

        foreach (Command command in commandsToRemove)
        {
            IBattleData.pendingCommands.Remove(command);
        }
    }

    void RefreshNearbyEnemies()
    {
        closeEnemies = closeEnemyTargeter.CalculateTargets(allies.GetChild(0).GetComponent<IActor>().getCollider2D.bounds.center).ToHashSet();

        farEnemies.Clear();

        int count = Mathf.Min(allies.childCount, IAllie.MaxActiveAlliesCount);
        for (int i = 0; i < count; i++)
            farEnemies.UnionWith(farEnemyTargeter.CalculateTargets(allies.GetChild(i).GetComponent<IActor>().getCollider2D.bounds.center).ToHashSet());
        
        enemyTargets.Clear();
        enemiesToRemove.Clear();

        foreach (Transform t in enemies)
        {
            enemyTargets.Add(t.GetComponent<IActor>());
            enemiesToRemove.Add(t.GetComponent<IActor>());
        }
        
        closeEnemies.ExceptWith(enemyTargets);
        enemiesToRemove.ExceptWith(farEnemies);

        foreach (IActor actor in closeEnemies)
        {
            actor.obj.transform.parent = enemies;
            actor.obj.GetComponent<IController>().SetGoal(StateDatabase.Instance.GetGoal("HostileState"));
        }

        foreach (IActor actor in enemiesToRemove)
        {
            actor.obj.transform.parent = null;
            actor.obj.GetComponent<IController>().SetGoal(StateDatabase.Instance.GetGoal("PatrolState"));
        }

        bool isTargetingEnemy = IBattleData.pendingCommands.Any(i => i.targets[0].obj.layer != i.user.obj.layer);
        allieCommands.Clear();

        foreach (Command command in IBattleData.pendingCommands)
            if (command.user.obj.layer == LayerMask.NameToLayer("Allie") && command.user.obj.layer != command.targets[0].obj.layer)
                allieCommands.Add(command);

        foreach (Command command in allieCommands)
            foreach (IActor actor in command.targets)
                if (!enemyTargets.Contains(actor))
                {
                    actor.obj.transform.parent = enemies;
                    actor.obj.GetComponent<IController>().SetGoal(StateDatabase.Instance.GetGoal("HostileState"));
                }

        if (isTargetingEnemy && !IBattleData.isInBattle)
        {
            //enter battle code here
            IBattleData.isInBattle = true;
            IPlayerControls controls = allies.GetComponent<IPlayerControls>();
            controls.SetUnselectedDefaultGoal(StateDatabase.Instance.GetGoal("BattleState"));
        }
        else if (farEnemies.Count == 0 && IBattleData.isInBattle)
        {
            //exit battle code here
            IBattleData.isInBattle = false;
            IPlayerControls controls = allies.GetComponent<IPlayerControls>();
            controls.SetUnselectedDefaultGoal(StateDatabase.Instance.GetGoal("FollowState"));
            controls.Refresh();
        }
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
