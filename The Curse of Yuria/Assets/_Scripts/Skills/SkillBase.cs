using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System.Collections.ObjectModel;

namespace TCOY.Skills
{
    public abstract class SkillBase : MonoBehaviour
    {
        [SerializeField] protected int power;
        [SerializeField] protected int cost;
        [SerializeField] protected float duration = float.PositiveInfinity;
        [SerializeField] protected ISkill.Group group = ISkill.Group.None;
        [SerializeField] protected ISkill.Type type = ISkill.Type.None;
        [SerializeField] protected ISkill.Element element = ISkill.Element.None;

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

        public GameObject getGameObject => gameObject;

        public int getPower => power;
        public int getCost => cost;
        public float getDuration => duration;
        public ISkill.Group getGroup => group;
        public ISkill.Type getType => type;
        public ISkill.Element getElement => element;

        public string getIdentifiers => name + '|' + group.ToString() + '|' + type.ToString() + '|' + element.ToString();

        protected virtual void Start()
        {
            IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            IFactory factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            target = transform.parent.GetComponent<IActor>();

            StartCoroutine(performAnimation());
        }

        protected virtual IEnumerator performAnimation()
        {
            user.getAnimator.Cast();

            while (user.getAnimator.isPerformingCommand)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return InstantiateAbility();
        }

        protected virtual IEnumerator InstantiateAbility()
        {
            ParticleSystem particleSystem = Instantiate(factory.GetParticleSystemPrefab(particleSystemName), transform).GetComponent<ParticleSystem>();
            Destroy(gameObject, particleSystem.main.duration);
            yield return null;
        }

        public virtual void SetUser(IActor user)
        {
            this.user = user;
        }

        protected virtual void OnDestroy()
        {
            getTarget.getStats.ApplySkillCalculation(power, getUser.getStats, group, type, element);

        }
    }
}