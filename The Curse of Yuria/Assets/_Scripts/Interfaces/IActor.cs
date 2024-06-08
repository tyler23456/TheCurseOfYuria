using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

public interface IActor
{
    bool useDefaultItems { get; set; }
    Collider2D getCollider2D { get; }
    GameObject obj { get; }
    IStats getStats { get; }
    IATBGuage getATBGuage { get; }
    List<Reactor> getCounters { get; }
    List<Reactor> getInterrupts { get; }
    IInventory getEquipment { get; }
    IInventory getScrolls { get; }
    IStatusEffects getStatusEffects { get; }
    Coroutine StartCoroutine(IEnumerator routine);
}
