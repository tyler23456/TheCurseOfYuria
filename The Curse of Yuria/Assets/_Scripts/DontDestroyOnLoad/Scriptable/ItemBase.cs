using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

namespace TCOY.DontDestroyOnLoad
{
    public abstract class ItemBase : ScriptableObject
    {
        [SerializeField] [HideInInspector] protected ulong guid;

        [SerializeField] protected Sprite _icon;
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected IItem.Category _category;
        [SerializeField] [TextArea(3, 10)] protected string info;

        [SerializeField] protected IItem.Group group = IItem.Group.None;
        [SerializeField] protected IItem.Type type = IItem.Type.None;
        [SerializeField] protected IItem.Element element = IItem.Element.None;
        [SerializeField] protected int power;
        [SerializeField] protected int cost;
        [SerializeField] protected float duration = float.PositiveInfinity;
        [SerializeField] List<GameObject> effects;
        [SerializeField] protected ParticleSystem particleSystem;

        [SerializeField] protected ItemSprite _itemSprite;
        [SerializeField] protected List<Modifier> modifiers;
        [SerializeField] protected List<Reactor> counters; //Reactor System
        [SerializeField] protected List<Reactor> interrupts;

        public ulong getGuid => guid;
        public string itemName { get { return name; } set { name = value; } }
        public Sprite icon { get { return _icon; } set { _icon = value; } }
        public GameObject prefab { get { return _prefab; } set { _prefab = value; } }
        public IItem.Category category { get { return _category; } set { _category = value; } }
        public string getInfo => info;

        public IItem.Group getGroup => group;
        public IItem.Type getType => type;
        public IItem.Element getElement => element;
        public int getPower => power;
        public int getCost => cost;
        public float getDuration => duration;

        public string getIdentifiers => name + '|' + group.ToString() + '|' + type.ToString() + '|' + element.ToString();

        public ItemSprite itemSprite { get { return _itemSprite; } set { _itemSprite = value; } }
        public List<Modifier> getModifiers => modifiers;
        public List<Reactor> getCounters => counters; 
        public List<Reactor> getInterrupts => interrupts;

        public virtual void Use(IActor user, IActor[] targets)
        {
            
        }
    }
}