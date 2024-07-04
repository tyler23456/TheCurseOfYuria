using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ward
{
    [SerializeField] IElementType elementType;
    [SerializeField] [Range(1, 500)] int amount = 5;

    public IElementType getElementType => elementType;
    public int getAmount => amount;
}
