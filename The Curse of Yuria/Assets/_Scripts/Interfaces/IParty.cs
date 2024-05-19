using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParty
{
    void AddMember(IAllie member);
    IAllie GetMember(string memberName);
}
