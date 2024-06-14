using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKOTargeter", menuName = "Targeters/KOTargeter")]
public class KOTargeter : RandomTargeter
{
    protected override void FilterResults(List<IActor> targets)
    {
        targets.RemoveAll(i => !i.getStatusEffects.Contains(StatFXDatabase.Instance.getKnockOut.name));
    }
}
