using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace TCOY.Abilities
{
    public abstract class AbilityBase : MonoBehaviour
    {
        [SerializeField] protected int power = 0;
        [SerializeField] protected float duration;
        [SerializeField] protected string type = "None";
        [SerializeField] protected string[] attributeEffects;

        protected float attack = 10;

        IGlobal global;
        IFactory factory;
        new ParticleSystem particleSystem;
        IActor user;
        IActor target;
        
        public int getPower => power;
        public float getDuration => duration;
        public string getType => type;

        public IGlobal getGlobal => global;
        public IFactory getFactory => factory;
        public ParticleSystem getVisualEffect => particleSystem;
        public IActor getUser => user;
        public IActor getTarget => target;
        
        public virtual void Start()
        {
            IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            IFactory factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();

            target = transform.parent.GetComponent<IActor>();
            attack = attack + power;

            
            Destroy(gameObject, particleSystem.main.duration);
        }
    }
}