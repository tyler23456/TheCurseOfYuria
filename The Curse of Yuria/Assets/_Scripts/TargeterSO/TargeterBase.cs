using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargeterBase : Targeter
{
    static LayerMask layerMask;
    
    protected static int colliderCount { get; private set; } = 0;
    protected static Collider2D[] colliders { get; private set; } = new Collider2D[10];
    protected static List<IActor> targets { get; private set; } = new List<IActor>();

    static IActor target = null;

    [SerializeField] Targeter.Party party;

    protected virtual bool canTargetKO => false;

    public override IActor[] CalculateTargets(Vector2 position, float targetCheckDistance = DefaultTargetCheckDistance)
    {
        switch (party)
        {
            case Targeter.Party.Allie:
                layerMask = LayerMask.GetMask("Allie");
                break;
            case Targeter.Party.Enemy:
                layerMask = LayerMask.GetMask("Enemy");
                break;
            case Targeter.Party.Both:
                layerMask = LayerMask.GetMask("Allie") | LayerMask.GetMask("Enemy");
                break;
        }

        colliderCount = Physics2D.OverlapCircleNonAlloc(position, DefaultTargetCheckDistance, colliders, layerMask);

        targets.Clear();
        for (int i = 0; i < colliderCount; i++)
        {
            target = colliders[i].GetComponent<IActor>();

            if (target == null)
                continue;

            if (target.getStatusEffects.Contains(StatFXDatabase.Instance.getKnockOut.name))
                continue;

            if (target.getDetection.getPriority < 0)
                continue;

            Vector2 direction = ((Vector2)target.getCollider2D.bounds.center - position).normalized;

            //if (Physics2D.Raycast(position, direction, DefaultTargetCheckDistance, LayerMask.GetMask("TileCollision")).collider == null)
                targets.Add(colliders[i].GetComponent<IActor>());
        }

        //FilterResults(targets);

        return targets.ToArray();
    }

    protected virtual void FilterResults(List<IActor> targets)
    {
        targets.RemoveAll(i => i.getStatusEffects.Contains(StatFXDatabase.Instance.getKnockOut.name));
    }



}
