using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        IGlobal global;
        IFactory factory;

        [SerializeField] List<StatusEffectBase> gameOverStatusEffects;

        public bool isRunning { get; set; } = false;

        bool isGameOver = false;

        void Start()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
            isGameOver = false;
        }

        void Update()
        {
            if (isGameOver)
                return;

            int partyMemberCount = Mathf.Clamp(3, 0, global.getPartyMemberCount);

            bool AreAllFrontLinePartyMembersPermanentlyInnactive = true;
            for (int i = 0; i < partyMemberCount; i++)
            {
                if (gameOverStatusEffects.All(statusEffect => !global.GetPartyMember(i).getStatusEffects.Contains(statusEffect.name)))
                {
                    AreAllFrontLinePartyMembersPermanentlyInnactive = false;
                    break;
                }
            }

            if (AreAllFrontLinePartyMembersPermanentlyInnactive)
            {
                isGameOver = true;
                global.CloseAllDisplays();
                global.getGameOverDisplay.gameObject.SetActive(true);
                return;
            }

            if (global.pendingCommands.Count == 0 || isRunning)
                return;

            isRunning = true;
            RunNextCommand();
        }

        void RunNextCommand()
        {
            ICommand command = global.pendingCommands.Dequeue();

            command.item.Use(command.user, command.targets);
            isRunning = false;
        }

    }
}