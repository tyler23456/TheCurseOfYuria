using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    IEnumerator Activate(List<IActor> actors);
}
