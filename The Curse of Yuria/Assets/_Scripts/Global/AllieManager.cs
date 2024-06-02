using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AddressableAssets;

public class AllieManager : MonoBehaviour
{
    public static AllieManager Instance { get; private set; }

    public IActor this[int index]
    {
        get => transform.GetChild(index).GetComponent<IActor>();
    }

    public int count => transform.childCount;
    public int selectedCount => Mathf.Min(3, 0, count - 1);

    private void Awake()
    {
        Instance = this;
    }

    public int GetSafeIndex(int index)
    {
        return Mathf.Clamp(index, 0, count - 1);
    }

    public int GetSafeSelectedIndex(int index)
    {
        return Mathf.Clamp(index, 0, selectedCount - 1);
    }

    public List<Command> CalculateCounters(Command command)
    {
        IActor actor = null;

        List<Command> results = new List<Command>();
        foreach (Transform t in transform)
        {
            actor = t.GetComponent<IActor>();

            foreach (Reactor reactor in actor.getCounters)
                if (command.targets[0].getParty == reactor.getParty && command.item.name == reactor.getItem.name)
                    results.Add(new Command(actor, reactor.getReaction, reactor.getTargeter.CalculateTargets(actor.getGameObject.transform.position)));
        }   
        return results;
    }

    public List<Command> CalculateInterrupts(Command command)
    {
        IActor actor = null;

        List<Command> results = new List<Command>();
        foreach (Transform t in transform)
        {
            actor = t.GetComponent<IActor>();

            foreach (Reactor reactor in actor.getInterrupts)
                if (command.targets[0].getParty == reactor.getParty && command.item.name == reactor.getItem.name) //targets can be null....need to fix that
                    results.Add(new Command(actor, reactor.getReaction, reactor.getTargeter.CalculateTargets(actor.getGameObject.transform.position)));
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
            actor.getGameObject.transform.parent = transform;
    }

    public void Add(IActor actor)
    {
        actor.getGameObject.transform.parent = transform;
    }



    public void SwapIndexes(int index1, int index2)
    {
        if (index1 == index2)
            return;
            
        int minIndex = Mathf.Min(index1, index2);
        int maxIndex = Mathf.Max(index1, index2);

        transform.GetChild(maxIndex).SetSiblingIndex(minIndex);
        transform.GetChild(minIndex + 1).SetSiblingIndex(maxIndex);
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

    public Vector3 GetPosition3DAt(int index)
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
            t.gameObject.SetActive(false);
            t.position = position;
            t.gameObject.SetActive(true);
        }
    }

    public void SetEulerAngleZ(float z)
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
            t.eulerAngles = new Vector3(0f, 0f, z);
            t.gameObject.SetActive(true);
        }
    }
}
