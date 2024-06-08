using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "NewObtainer", menuName = "Cutscene/Obtainer")]
public class Obtainer : ActionBase, IAction
{
    [SerializeField] List<Entry> entries;

    public override IEnumerator Activate()
    {
        base.Activate();

        foreach (Entry entry in entries)
            InventoryManager.Instance.AddItem(entry.item.name, entry.count);

        yield return new WaitForEndOfFrame();
    }
}
