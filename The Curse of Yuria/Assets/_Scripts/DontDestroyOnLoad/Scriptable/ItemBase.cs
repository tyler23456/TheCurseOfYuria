using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class ItemBase : ScriptableObject
    {
        [SerializeField] [HideInInspector] protected ulong guid;

        [SerializeField] protected Sprite icon;
        [SerializeField] protected Sprite sprite;
        [SerializeField] protected GameObject prefab;

        public virtual void Use(IActor user, IActor[] targets)
        {

        }
    }
}