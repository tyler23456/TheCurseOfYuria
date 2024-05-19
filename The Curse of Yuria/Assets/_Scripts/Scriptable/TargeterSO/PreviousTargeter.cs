using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewPreviousTargeter", menuName = "Targeters/PreviousTargeter")]
public class PreviousTargeter : TargeterBase
{
    enum Type { user, targets }

    [SerializeField] Type type;
    
    public override List<IActor> CalculateTargets(Vector2 position)
    {
        base.CalculateTargets(position);

        List<IActor> results = new List<IActor>();

        IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

        

        return results;
    }


}
