using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParty
{
    void AddMember(IPlayer member);
    IPlayer GetMember(string memberName);
}
