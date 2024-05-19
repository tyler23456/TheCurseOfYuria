using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObtainer", menuName = "Dialogue/Obtainer")]
public class Obtainer : ActionBase, IAction
{
    [SerializeField] List<ItemBase> items;

    public override IEnumerator Activate(IGlobal global, IFactory factory, List<IActor> actors)
    {
        base.Activate(global, factory, actors);

        foreach (ItemBase item in items)
            global.inventories[item.category].Add(item.name);

        yield return new WaitForEndOfFrame();
    }
}
