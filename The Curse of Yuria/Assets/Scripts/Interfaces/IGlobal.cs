using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobal
{
    IParty getParty { get; }

    IInventory getHelmets { get; }
    IInventory getEarrings { get; }
    IInventory getGlasses { get; }
    IInventory getMasks { get; }
    IInventory getMeleeWeapons { get; }
    IInventory getCapes { get; }
    IInventory getBackArmor { get; }
    IInventory getArmor { get; }
    IInventory getArmorShield { get; }
    IInventory getBows { get; }
}
