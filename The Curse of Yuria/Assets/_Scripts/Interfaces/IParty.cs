using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParty
{
    void AddMember(IPartyMember member);
    IPartyMember GetMember(string memberName);
}
