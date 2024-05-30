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
            
            Vector3 eulerAngles = transform.eulerAngles;
            Vector3 targetEulerAngles = player.getGameObject.transform.GetChild(0).eulerAngles;

            if (difference >= 0)
            {
                eulerAngles.y = 0f;
                targetEulerAngles.y = 180;
            }          
            else
            {
                eulerAngles.y = 180f;
                targetEulerAngles.y = 0f;
            }
                
            transform.eulerAngles = eulerAngles;
            player.getGameObject.transform.GetChild(0).eulerAngles = targetEulerAngles;

            promptBranchers[0].getAction.onStart = () => animator.SetInteger("State", 8);
            promptBranchers[0].getAction.onStop = () => animator.SetInteger("State", 0);
            CutsceneDisplay.Instance.ShowExclusivelyInParent(new ActionBase[] { promptBranchers[0].getAction });
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