using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IATBGuage
{
    void Reset();
    bool isActive { get; set; }
}