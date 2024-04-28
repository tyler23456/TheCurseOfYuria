using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Scroll : ItemBase, IItem
    {
        [SerializeField] protected int power;
        [SerializeField] protected int cost;
        [SerializeField] protected float duration = float.PositiveInfinity;
        [SerializeField] protected ISkill.Group group = ISkill.Group.None;
        [SerializeField] protected ISkill.Type type = ISkill.Type.None;
        [SerializeField] protected ISkill.Element element = ISkill.Element.None;
        [SerializeField] protected ParticleSystem particleSystem;
        [SerializeField] List<GameObject> effects;

        public int getPower => power;
        public int getCost => cost;
        public float getDuration => duration;
        public ISkill.Group getGroup => group;
        public ISkill.Type getType => type;
        public ISkill.Element getElement => element;
        

        public string getIdentifiers => name + '|' + group.ToString() + '|' + type.ToString() + '|' + element.ToString();

        public override void Use(IActor user, IActor[] targets)
        {     
            IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            user.getAnimator.Cast();
            global.StartCoroutine(performAnimation(user, targets));
        }    

        protected virtual IEnumerator performAnimation(IActor user, IActor[] targets)
        {
            while (user.getAnimator.isPerformingCommand)
                yield return new WaitForEndOfFrame();

            foreach (IActor target in targets)
                yield return PerformEffect(user, target);  //may need to start a new coroutine for this? 
        }

        protected virtual IEnumerator PerformEffect(IActor user, IActor target)
        {
            ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.getGameObject.transform).GetComponent<ParticleSystem>();
            Destroy(particleSystem.gameObject, particleSystem.main.duration);

            while (particleSystem != null)
                yield return new WaitForEndOfFrame();

            target.getStats.ApplySkillCalculation(power, user.getStats, group, type, element);
        }


    }
}