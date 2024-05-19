using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargeterBase : ScriptableObject
{
    public enum Party { Allie, Enemy, Both }

    static LayerMask layerMask; 
    static float targetCheckDistance = 20f;

    protected static int colliderCount { get; private set; } = 0;
    protected static Collider2D[] colliders { get; private set; } = new Collider2D[10];
    protected static List<IActor> targets { get; private set; } = new List<IActor>();

    static IActor target = null;

    [SerializeField] Party party;

    public virtual List<IActor> CalculateTargets(Vector2 position)
    {
        switch (party)
        {
            case Party.Allie:
                layerMask = LayerMask.NameToLayer("Allie");
                break;
            case Party.Enemy:
                layerMask = LayerMask.NameToLayer("Enemy");
                break;
            case Party.Both:
                layerMask = LayerMask.NameToLayer("Allie") | LayerMask.NameToLayer("Enemy");
                break;
        }

        colliderCount = Physics2D.OverlapCircleNonAlloc(position, targetCheckDistance, colliders, ~layerMask);

        targets.Clear();
        for (int i = 0; i < colliderCount; i++)
        {
            target = colliders[i].GetComponent<IActor>();

            if (target == null)
                continue;

            if (target.getStatusEffects.Contains("KnockOut"))
                continue;

            targets.Add(colliders[i].GetComponent<IActor>());
        }

        return targets;
    }
}
