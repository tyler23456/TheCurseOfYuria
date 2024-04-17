using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class AbilityBase : ScriptableObject, IAbility
    {
        [SerializeField] protected int power = 0;
        [SerializeField] protected string type = "None";
        [SerializeField] protected List<AttributeEffects> attributeEffects;

        public virtual void Use(IActor user, IActor[] targets)
        {
            IFactory factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            
        }



        public class AttributeEffects
        {
            public string attributeName { get; set; }
            public int attributeOffset { get; set; }
        }

    }
}