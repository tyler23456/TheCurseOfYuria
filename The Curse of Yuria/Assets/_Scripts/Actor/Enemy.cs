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

            foreach (string skill in skills.GetNames())
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
            IItem.Type skillType = factory.GetItem(skill).getType;
            IActor target = null;

            if (skillType == IItem.Type.Damage)
            {
                target = global.getParty[Random.Range(0, global.getParty.Count)];
            }
            else
            {
                target = this;
            }

            Command newCommand = new Command(this, factory.GetItem(combinedSkills.Peek()), new IActor[] { target });
            global.commandQueue.Enqueue(newCommand);
            combinedSkills.Enqueue(combinedSkills.Dequeue());
        }
    }
}