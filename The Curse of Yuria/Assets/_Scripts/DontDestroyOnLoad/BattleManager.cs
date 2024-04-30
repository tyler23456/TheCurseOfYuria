using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.BattleSystem
{
    public class BattleManager : MonoBehaviour
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
            ICommand command = global.commandQueue.Dequeue();

            command.item.Use(command.user, command.targets);
            isRunning = false;
        }

    }
}