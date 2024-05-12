using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

public interface IActor
{
    Character getCharacter { get; }
    Collider2D getCollider2D { get; }
    GameObject getGameObject { get; }
    IPosition getPosition { get; }
    IRotation getRotation { get; }
    IStats getStats { get; }
    IATBGuage getATBGuage { get; }
    List<IReactor> counters { get; }
    List<IReactor> interrupts { get; }
    IInventory getEquipment { get;}
    IInventory getSkills { get; }
    IAnimator getAnimator { get; }
    IStatusEffects getStatusEffects { get; }
    Coroutine StartCoroutine(IEnumerator routine);
}
