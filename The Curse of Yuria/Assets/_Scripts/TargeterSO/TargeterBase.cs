using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargeterBase : ScriptableObject
{
    public enum Party { Allie, Enemy, Both }

    static LayerMask layerMask; 
    static float targetCheckDistance = 30f;

    protected static int colliderCount { get; private set; } = 0;
    protected static Collider2D[] colliders { get; private set; } = new Collider2D[10];
    protected static List<IActor> targets { get; private set; } = new List<IActor>();

    static IActor target = null;

    [SerializeField] Party party;

    protected virtual bool canTargetKO => false;

    public virtual List<IActor> CalculateTargets(Vector2 position)
    {
        switch (party)
        {
            case Party.Allie:
                layerMask = LayerMask.GetMask("Allie");
                break;
            case Party.Enemy:
                layerMask = LayerMask.GetMask("Enemy");
                break;
            case Party.Both:
                layerMask = LayerMask.GetMask("Allie") | LayerMask.GetMask("Enemy");
                break;
        }

        colliderCount = Physics2D.OverlapCircleNonAlloc(position, targetCheckDistance, colliders, layerMask);

        targets.Clear();
        for (int i = 0; i < colliderCount; i++)
        {
            target = colliders[i].GetComponent<IActor>();

            if (target == null)
                continue;

            if (target.getStatusEffects.Contains("KnockOut"))
                continue;

            if (target.getDetection.getPriority < 0)
                continue;

            targets.Add(colliders[i].GetComponent<IActor>());
        }

        FilterResults(targets);

        return targets;
    }

    protected virtual void FilterResults(List<IActor> targets)
    {
        targets.RemoveAll(i => i.getStatusEffects.Contains(StatFXDatabase.Instance.getKnockOut.name));
    }



}
