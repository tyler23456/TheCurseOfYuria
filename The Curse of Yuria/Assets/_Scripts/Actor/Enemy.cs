using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    public class Enemy : Actor, IEnemy
    {
        Queue<string> combinedSkills = new Queue<string>();


        protected new void Start()
        {
            base.Start();

            combinedSkills.Enqueue(attack);

            foreach (string skill in magic.GetNames())
                combinedSkills.Enqueue(skill);

            foreach (string skill in techniques.GetNames())
                combinedSkills.Enqueue(skill);

            aTBGuage.OnATBGuageFilled += MakeADecision;
        }

        protected new void Update()
        {
            base.Update();
        }

        void MakeADecision()
        {
            string skill = combinedSkills.Peek();
            ISkill.Type skillType = factory.GetAbilityPrefab(skill).getType;
            IActor target = null;

            if (skillType == ISkill.Type.Damage)
            {
                target = global.getParty[Random.Range(0, global.getParty.Count)];
            }
            else
            {
                target = this;
            }

            //need to decide a target;
            global.commandQueue.Enqueue((this, combinedSkills.Peek(), target));
        }
    }
}