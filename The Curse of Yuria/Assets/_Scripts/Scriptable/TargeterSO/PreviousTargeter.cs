using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewPreviousTargeter", menuName = "Targeters/PreviousTargeter")]
public class PreviousTargeter : TargeterBase
{
    [SerializeField] int previousUserCount = 1;
    
    public override List<IActor> CalculateTargets(Vector2 position)
    {
        base.CalculateTargets(position);

        List<IActor> results = new List<IActor>();

        IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

        if (global.successfulSubcommands.Count < previousUserCount)
            return null;

        for (int i = previousUserCount - 1; i > -1; i++)
            results.Add(global.successfulSubcommands[global.successfulSubcommands.Count - 1 - i].user);

        return results;
    }


}
