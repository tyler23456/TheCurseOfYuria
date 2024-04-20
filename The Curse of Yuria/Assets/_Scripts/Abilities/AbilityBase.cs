using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System.Collections.ObjectModel;

namespace TCOY.Abilities
{
    public abstract class AbilityBase : MonoBehaviour
    {
        [SerializeField] protected int power;
        [SerializeField] protected int cost;
        [SerializeField] protected float duration = float.PositiveInfinity;
        [SerializeField] protected IAbility.Group group = IAbility.Group.None;
        [SerializeField] protected IAbility.Type type = IAbility.Type.None;
        [SerializeField] protected IAbility.Element element = IAbility.Element.None;

        [SerializeField, HideInInspector] protected string identifiers;

        protected float attack = 10;

        IGlobal global;
        IFactory factory;
        IActor user;
        IActor target;

        protected virtual string particleSystemName => "Default";

        public IGlobal getGlobal => global;
        public IFactory getFactory => factory;
        public IActor getUser => user;
        public IActor getTarget => target;

        public string getIdentifiers => name + '|' + group.ToString() + '|' + type.ToString() + '|' + element.ToString();

        protected virtual void Start()
        {
            IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            IFactory factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            target = transform.parent.GetComponent<IActor>();

            ParticleSystem particleSystem = Instantiate(factory.particleSystemPrefabs[particleSystemName], transform).GetComponent<ParticleSystem>();
            Destroy(gameObject, particleSystem.main.duration);           
        }

        protected virtual void OnDestroy()
        {
            getTarget.getStats.ApplyAbility(power, getUser.getStats, group, type, element);

        }
    }
}