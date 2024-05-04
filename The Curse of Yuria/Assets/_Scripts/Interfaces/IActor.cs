using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    Collider2D getCollider2D { get; }
    GameObject getGameObject { get; }
    IPosition getPosition { get; }
    IRotation getRotation { get; }
    IStats getStats { get; }
    IATBGuage getATBGuage { get; }
    List<IReactor> counters { get; }
    List<IReactor> interrupts { get; }
    IEquipment getEquipment { get;}
    IInventory getSkills { get; }
    IAnimator getAnimator { get; }
}
