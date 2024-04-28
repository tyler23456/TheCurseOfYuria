using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public abstract class ItemBase : ScriptableObject
    {
        [SerializeField] [HideInInspector] protected ulong guid;

        [SerializeField] protected Sprite icon;
        [SerializeField] protected Sprite[] sprites;
        [SerializeField] protected GameObject prefab;
        [SerializeField] [TextArea(3, 10)] protected string info;

        public virtual void Use(IActor user, IActor[] targets)
        {

        }

        public void SetIcon(Sprite icon)
        {
            this.icon = icon;
        }

        public void SetSprite(Sprite[] sprites)
        {
            this.sprites = sprites;
        }

        public void SetPrefab(GameObject prefab)
        {
            this.prefab = prefab;
        }
    }
}