using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class Actors
{
    Transform parent;
    List<IActor> actors = new List<IActor>();

    public IActor this[int index]
    {
        get => actors[index];
        set => actors[index] = value;
    }

    public int count => actors.Count;

    public Actors(Transform parent = null)
    {
        this.parent = parent;
    }
        

    public void Clear()
    {
        actors.Clear();
    }

    public List<Command> CalculateCounters(Command command)
    {
        List<Command> results = new List<Command>();
        foreach (IActor actor in actors)
            foreach (Reactor reactor in actor.counters)            
                if (command.user.getParty == reactor.getParty && command.item.name == reactor.getItem.name)
                    results.Add(new Command(actor, reactor.getItem, reactor.getTargeter.CalculateTargets(actor.getGameObject.transform.position)));
        return results;
    }

    public List<Command> CalculateInterrupts(Command command)
    {
        List<Command> results = new List<Command>();
        foreach (IActor actor in actors)
            foreach (Reactor reactor in actor.interrupts)
                if (command.user.getParty == reactor.getParty && command.item.name == reactor.getItem.name)
                    results.Add(new Command(actor, reactor.getItem, reactor.getTargeter.CalculateTargets(actor.getGameObject.transform.position)));
        return results;
    }

    public void DestroyAndClear()
    {
        foreach (IActor actor in actors)
            GameObject.Destroy(actor.getGameObject);

        actors.Clear();
    }

    public void Set(List<IActor> actors)
    {
        this.actors = actors;

        foreach (IActor actor in actors)
            actor.getGameObject.transform.parent = parent;
    }

    public void Add(IActor actor)
    {
        actor.getGameObject.transform.parent = parent;
        actors.Add(actor);
    }

    public void Insert(int index, IActor actor)
    {
        actor.getGameObject.transform.parent = parent;
        actors.Insert(index, actor);
    }

    public void Reorder()
    {

    }

    public void ForEach(Action<IActor> action)
    {
        foreach (IActor actor in actors)
            action.Invoke(actor);
    }

    public bool AllContainAnyOf(List<StatusEffectBase> statusEffects)
    {
        foreach (IActor actor in actors)
            if (statusEffects.All(statusEffect => !actor.getStatusEffects.Contains(statusEffect.name)))
                return false;
        return true;
    }

    public Vector2 GetPositionAt(int index)
    {
        return actors[index].getGameObject.transform.position;
    }

    public float GetEulerAngleZAt(int index)
    {
        return actors[index].getGameObject.transform.eulerAngles.z;
    }

    public void SetPosition(Vector2 position)
    {
        foreach (IActor actor in actors)
        {
            actor.getGameObject.SetActive(false);
            actor.getGameObject.transform.position = position;
            actor.getGameObject.SetActive(true);
        }
    }

    public void SetEulerAngleZ(float z)
    {
        foreach (IActor actor in actors)
        {
            actor.getGameObject.SetActive(false);
            actor.getGameObject.transform.eulerAngles = new Vector3(0f, 0f, z);
            actor.getGameObject.SetActive(true);
        }
    }
}
