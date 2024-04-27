using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Supply : Skill
    {
        public override void Use(IActor user, IActor[] targets)
        {
            IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            global.getSupplies.Remove(sprite.name);
            global.StartCoroutine(performAnimation(user, targets));
        }
    }
}