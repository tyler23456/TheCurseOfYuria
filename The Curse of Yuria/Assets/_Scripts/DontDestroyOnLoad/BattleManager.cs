using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        IGlobal global;

        [SerializeField] List<StatusEffectBase> gameOverStatusEffects;

        void Start()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
        }

        void Update()
        {
            if (IGlobal.gameState != IGlobal.GameState.Playing)
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
                global.ToggleDisplay(IGlobal.Display.GameOverDisplay);
                return;
            }

            if (global.pendingCommands.Count == 0)
                return;

            RunNextCommand();
        }

        void RunNextCommand()
        {
            ICommand command = global.pendingCommands.Dequeue();

            if (command.user == null)
                return;

            command.user.StartCoroutine(command.item.Use(command.user, command.targets));
        }

    }
}