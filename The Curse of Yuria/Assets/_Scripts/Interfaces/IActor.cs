using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

public interface IActor
{
    void Initialize();
    TargeterBase.Party getParty { get; }
    Character getCharacter { get; }
    Collider2D getCollider2D { get; }
    GameObject getGameObject { get; }
    IPosition getPosition { get; }
    IRotation getRotation { get; }
    IStats getStats { get; }
    IATBGuage getATBGuage { get; }
    List<Reactor> counters { get; }
    List<Reactor> interrupts { get; }
    IInventory getEquipment { get;}
    IInventory getSkills { get; }
    IAnimator getAnimator { get; }
    IStatusEffects getStatusEffects { get; }
    Coroutine StartCoroutine(IEnumerator routine);
}
