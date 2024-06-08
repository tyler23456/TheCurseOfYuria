using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public IEnemy this[int index]
    {
        get => transform.GetChild(index).GetComponent<IEnemy>();
    }

    public int count => transform.childCount;

    private void Awake()
    {
        Instance = this;
    }

    public List<Command> CalculateCounters(Command command)
    {
        IEnemy actor = null;

        List<Command> results = new List<Command>();
        foreach (Transform t in transform)
        {
            actor = t.GetComponent<IEnemy>();

            foreach (Reactor reactor in actor.getCounters)
                if (((1 << command.targets[0].obj.layer) & reactor.getMask) != 0 && command.item.name == reactor.getItemName)
                    results.Add(new Command(actor, reactor.getReaction, reactor.getTargeter.CalculateTargets(actor.obj.transform.position)));
        }
        return results;
    }

    public List<Command> CalculateInterrupts(Command command)
    {
        IEnemy actor = null;

        List<Command> results = new List<Command>();
        foreach (Transform t in transform)
        {
            actor = t.GetComponent<IEnemy>();

            foreach (Reactor reactor in actor.getInterrupts)
                if (((1 << command.targets[0].obj.layer) & reactor.getMask) != 0 && command.item.name == reactor.getItemName) //targets can be null....need to fix that
                    results.Add(new Command(actor, reactor.getReaction, reactor.getTargeter.CalculateTargets(actor.obj.transform.position)));
        }
        return results;
    }

    public void DestroyAll()
    {
        foreach (Transform t in transform)
            Destroy(t.gameObject);
    }

    public void Set(List<IActor> actors)
    {
        foreach (IActor actor in actors)
            actor.obj.transform.parent = transform;
    }

    public void Add(IActor actor)
    {
        actor.obj.transform.parent = transform;
    }

    public void SwapIndexes(int index1, int index2)
    {

    }

    public void ForEach(Action<IActor> action)
    {
        foreach (Transform t in transform)
            action.Invoke(t.GetComponent<IActor>());
    }

    public bool AllContainAnyOf(List<StatusEffectBase> statusEffects)
    {
        foreach (Transform t in transform)
            if (statusEffects.All(statusEffect => !t.GetComponent<IActor>().getStatusEffects.Contains(statusEffect.name)))
                return false;
        return true;
    }

    public Vector2 GetPositionAt(int index)
    {
        return transform.GetChild(index).position;
    }

    public float GetEulerAngleZAt(int index)
    {
        return transform.GetChild(index).eulerAngles.z;
    }

    public void SetPosition(Vector2 position)
    {
        foreach (Transform t in transform)
        {
            transform.gameObject.SetActive(false);
            transform.position = position;
            transform.gameObject.SetActive(true);
        }
    }

    public void SetEulerAngleZ(float z)
    {
        foreach (Transform t in transform)
        {
            transform.gameObject.SetActive(false);
            transform.eulerAngles = new Vector3(0f, 0f, z);
            transform.gameObject.SetActive(true);
        }
    }
}
