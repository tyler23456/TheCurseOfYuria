using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AddressableAssets;

public class AllieManager : MonoBehaviour
{
    public static AllieManager Instance { get; private set; }

    public IAllie this[int index]
    {
        get => transform.GetChild(index).GetComponent<IAllie>();
    }

    public int count => transform.childCount;
    public int selectedCount => Mathf.Min(3, count);

    private void Awake()
    {
        Instance = this;
    }

    public IAllie First()
    {
        return Instance[0];
    }

    public void Refresh()
    {
            for (int i = 0; i < selectedCount; i++)
            transform.GetChild(i).gameObject.SetActive(true);

        for (int i = 3; i < count; i++)
            transform.GetChild(i).gameObject.SetActive(false);
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
        IAllie actor = null;
         
        List<Command> results = new List<Command>();
        foreach (Transform t in transform)
        {
            actor = t.GetComponent<IAllie>();
            
            foreach (Reactor reactor in actor.getCounters)
                if (((1 << command.targets[0].obj.layer) & reactor.getMask) != 0 && command.item.name == reactor.getItemName)
                    results.Add(new Command(actor, reactor.getReaction, reactor.getTargeter.CalculateTargets(actor.obj.transform.position)));
        }
        return results;
    }

    public List<Command> CalculateInterrupts(Command command)
    {
        IAllie actor = null;

        List<Command> results = new List<Command>();
        foreach (Transform t in transform)
        {
            actor = t.GetComponent<IAllie>();

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

    public void Set(List<IAllie> actors)
    {
        foreach (IActor actor in actors)
            actor.obj.transform.parent = transform;
    }

    public void Add(IAllie actor)
    {
        actor.obj.transform.parent = transform;
    }

    public void AddAndRefresh(IAllie actor)
    {
        actor.obj.transform.parent = transform;
        Refresh();
    }

    public void MoveIndex(int fromIndex, int toIndex)
    {
        transform.GetChild(fromIndex).SetSiblingIndex(toIndex);
        Refresh();
    }

    public void SwapIndexes(int index1, int index2)
    {
        if (index1 == index2)
            return;
            
        int minIndex = Mathf.Min(index1, index2);
        int maxIndex = Mathf.Max(index1, index2);

        transform.GetChild(maxIndex).SetSiblingIndex(minIndex);
        transform.GetChild(minIndex + 1).SetSiblingIndex(maxIndex);
        Refresh();
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
        bool previousActive = false;
        foreach (Transform t in transform)
        {
            previousActive = t.gameObject.activeSelf;
            t.gameObject.SetActive(false);
            t.position = position;
            t.gameObject.SetActive(previousActive);
        }
    }

    public void SetEulerAngleZ(float z)
    {
        bool previousActive = false;
        foreach (Transform t in transform)
        {
            previousActive = t.gameObject.activeSelf;
            t.gameObject.SetActive(false);
            t.eulerAngles = new Vector3(0f, 0f, z);
            t.gameObject.SetActive(previousActive);
        }
    }
}
