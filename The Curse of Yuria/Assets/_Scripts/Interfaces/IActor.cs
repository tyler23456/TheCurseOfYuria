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
    List<IReactor> counters { get; }
    List<IReactor> interrupts { get; }
    IEquipment getEquipment { get;}
    public IInventory getMagic { get; }
    public IInventory getTechniques { get; }
}
