using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [SerializeField] TargeterBase enemyTargeter;
    [SerializeField] List<StatusEffectBase> gameOverStatusEffects;

    public Queue<IActor> aTBGuageFilledQueue { get; set; } = new Queue<IActor>();
    public LinkedList<Command> pendingCommands { get; set; } = new LinkedList<Command>();
    public LinkedList<Command> successfulCommands { get; set; } = new LinkedList<Command>();

    public void Awake()
    {
        Instance = this;
    }

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

            if (pendingCommands.Count == 0)
                continue;

            Command command = pendingCommands.First();
            pendingCommands.RemoveFirst();

            if (command.user == null)
                continue;

            command.user.StartCoroutine(command.item.Use(command.user, command.targets));
            successfulCommands.AddLast(command);
            yield return new WaitForSeconds(1f);
        }
    }

    void CheckForCounters()
    {
        if (successfulCommands.Count == 0 || !successfulCommands.Last().isCounterable)
            return;

        List<Command> counters = AllieManager.Instance.CalculateCounters(successfulCommands.Last());
        List<Command> counters2 = EnemyManager.Instance.CalculateCounters(successfulCommands.Last());
        counters.AddRange(counters2);

        successfulCommands.Last().isCounterable = false;

        foreach (Command command in counters)
        {
            command.isCounterable = false;
            pendingCommands.AddLast(command);
        }
    }

    void CheckForInterrupts()
    {
        if (pendingCommands.Count == 0 || !pendingCommands.First().isInterruptable)
            return;

        List<Command> interrupts = AllieManager.Instance.CalculateInterrupts(pendingCommands.First());
        List<Command> interrupts2 = EnemyManager.Instance.CalculateInterrupts(pendingCommands.First());
        interrupts.AddRange(interrupts2);

        pendingCommands.First().isInterruptable = false;

        foreach (Command command in interrupts)
        {
            command.isInterruptable = false;
            pendingCommands.AddFirst(command);
        }
    }

    void RefreshNearbyEnemies()
    {
        List<IActor> targets = enemyTargeter.CalculateTargets(AllieManager.Instance.GetPositionAt(0));
        EnemyManager.Instance.Set(targets);
    }

    void CheckForGameOver()
    {
        if (!GameStateManager.Instance.isPlaying)
            return;

        if (AllieManager.Instance.AllContainAnyOf(gameOverStatusEffects))
            GameOverDisplay.Instance.ShowExclusivelyInParent();
    }
}
