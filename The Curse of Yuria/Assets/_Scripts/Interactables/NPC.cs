using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using HeroEditor.Common.Enums;

namespace TCOY.DontDestroyOnLoad
{
    public class NPC : InteractableBase, IInteractablePointer
    {
        [SerializeField] List<Equipable> equipment;
        [SerializeField] List<PromptBrancher> promptBranchers;

        Character character;
        Animator animator;

        protected void OnValidate()
        {
            character = GetComponent<Character>();
            character.UnEquip(EquipmentPart.Helmet);
            character.UnEquip(EquipmentPart.Earrings);
            character.UnEquip(EquipmentPart.Glasses);
            character.UnEquip(EquipmentPart.Mask);
            character.UnEquip(EquipmentPart.MeleeWeapon1H);
            character.UnEquip(EquipmentPart.MeleeWeapon2H);
            character.UnEquip(EquipmentPart.Cape);
            character.UnEquip(EquipmentPart.Armor);
            character.UnEquip(EquipmentPart.Shield);
            character.UnEquip(EquipmentPart.Bow);
            foreach (ItemBase item in equipment)
                character.Equip(item.itemSprite, item.itemType.part);
        }

        protected new void Start()
        {
            base.Start();

            character = GetComponent<Character>();
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        public override void Interact(IActor player)
        {
            base.Interact(player);

            float difference = player.obj.transform.position.x - this.transform.position.x;
            
            Vector3 eulerAngles = transform.eulerAngles;
            Vector3 targetEulerAngles = player.obj.transform.eulerAngles;

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
            player.obj.transform.eulerAngles = targetEulerAngles;

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