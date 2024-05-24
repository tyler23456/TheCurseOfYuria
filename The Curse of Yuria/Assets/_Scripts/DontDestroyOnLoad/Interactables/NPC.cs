using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.DontDestroyOnLoad
{
    public class NPC : InteractableBase
    {
        [SerializeField] List<Equipable> equipment;
        [SerializeField] List<PromptBrancher> promptBranchers;

        Character character;
        Animator animator;

        protected new void Start()
        {
            base.Start();

            character = GetComponent<Character>();
            animator = transform.GetChild(0).GetComponent<Animator>();

            foreach (Equipable equipable in equipment)
                character.Equip(equipable.itemSprite, equipable.itemType.part);
        }

        public override void Interact(IActor player)
        {
            base.Interact(player);

            float difference = player.getGameObject.transform.position.x - this.transform.position.x;

            //may need code in here that turns off walking animations and other animations

            Vector3 eulerAngles = transform.eulerAngles;

            if (difference >= 0)
                eulerAngles.y = 0f;
            else
                eulerAngles.y = 180f;

            transform.eulerAngles = eulerAngles;

            promptBranchers[0].getAction.onStart = () => animator.SetInteger("State", 8);
            promptBranchers[0].getAction.onStop = () => animator.SetInteger("State", 0);
            global.cutsceneActions.Enqueue(promptBranchers[0].getAction);
            global.ToggleDisplay(IGlobal.Display.CutsceneDisplay);
        }

        protected void Update()
        {
            //npc walking animation or other idle animation
        }

        [System.Serializable]
        public class PromptBrancher
        {
            [SerializeField] QuestBase unlockingQuest;
            [SerializeField] ActionBase action;

            public QuestBase getUnlockingQuest => unlockingQuest;
            public ActionBase getAction => action;
        }
    }
}