using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

public interface IActor
{
    TargeterBase.Party getParty { get; }
    Character getCharacter { get; }
    Collider2D getCollider2D { get; }
    GameObject getGameObject { get; }
    IPosition getPosition { get; }
    IRotation getRotation { get; }
    IStats getStats { get; }
    IATBGuage getATBGuage { get; }
    List<Reactor> getCounters { get; }
    List<Reactor> getInterrupts { get; }
    IInventory getEquipment { get;}
    IInventory getScrolls { get; }
    IAnimator getAnimator { get; }
    IStatusEffects getStatusEffects { get; }
    Coroutine StartCoroutine(IEnumerator routine);
}
