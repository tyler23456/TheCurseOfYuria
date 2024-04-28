using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public abstract class ItemBase : ScriptableObject
    {
        [SerializeField] [HideInInspector] protected ulong guid;

        [SerializeField] protected Sprite _icon;
        [SerializeField] protected GameObject _prefab;
        [SerializeField] [TextArea(3, 10)] protected string info;

        public Sprite icon { get { return _icon; } set { _icon = value; }  }
        public GameObject prefab { get { return _prefab; } set { _prefab = value; } }
        public string getInfo => info;

        public virtual void Use(IActor user, IActor[] targets)
        {
            
        }

        public void SetIcon(Sprite icon)
        {
            this.icon = icon;
        }

        public void SetPrefab(GameObject prefab)
        {
            this.prefab = prefab;
        }
    }
}