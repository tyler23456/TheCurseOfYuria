using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.BattleSystem
{
    public class BattleManager : MonoBehaviour, IBattleManager
    {
        IGlobal global;
        IFactory factory;
        public bool isRunning { get; set; } = false;


        void Start()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
        }

        void Update()
        {
            if (global.commandQueue.Count == 0 || isRunning)
                return;

            isRunning = true;
            RunNextCommand();
        }

        void RunNextCommand()
        {
            (IActor user, string skill, IActor target) command = global.commandQueue.Dequeue();

            ISkill skillPrefab = factory.GetAbilityPrefab(command.skill);
            GameObject skillObj = Instantiate(skillPrefab.getGameObject, command.target.getGameObject.transform);
            ISkill skill = skillObj.GetComponent<ISkill>();
            skill.SetUser(command.user);
            isRunning = false;
        }

    }
}