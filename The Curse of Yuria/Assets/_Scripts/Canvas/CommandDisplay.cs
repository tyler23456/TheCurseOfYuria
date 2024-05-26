using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;

namespace TCOY.Canvas
{
    public class CommandDisplay : MonoBehaviour
    {
        [SerializeField] Button buttonPrefab;
        [SerializeField] RectTransform grid;
        [SerializeField] Button attackTab;
        [SerializeField] Button magicTab;
        [SerializeField] Button itemTab;

        IActor currentPartyMember;
        string commandName = "None";
        IActor target = null;

        InventoryUI skillInventoryUI;
        InventoryUI itemInventoryUI;

        public enum State { attack, skill, item }
        State currentState = State.attack;

        public State getState => currentState;

        void OnEnable()
        {
            skillInventoryUI = new InventoryUI();
            itemInventoryUI = new InventoryUI();

            currentPartyMember = Global.instance.aTBGuageFilledQueue.Peek();

            attackTab.onClick.RemoveAllListeners();
            magicTab.onClick.RemoveAllListeners();
            itemTab.onClick.RemoveAllListeners();

            attackTab.onClick.AddListener(() => OnClickAttack());
            magicTab.onClick.AddListener(() => OnClickSkill());
            itemTab.onClick.AddListener(() => OnClickItems());

            commandName = "None";

            OnClickAttack();

            Global.instance.gameState = Global.GameState.Paused;
        }

        private void OnDisable()
        {
            Global.instance.gameState = Global.GameState.Playing;
        }

        public void OnClickAttack()
        {
            currentState = State.attack;

            string weapon = currentPartyMember.getEquipment.Find(i => 
            Factory.instance.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon1H ||
            Factory.instance.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon2H ||
            Factory.instance.GetItem(i).itemType.part == EquipmentPart.Bow);

            if (weapon == null)
                return;
            
            OnSelectCommand(weapon);
        }

        public void OnClickSkill()
        {
            currentState = State.skill;
            /*globalInventoryUI.grid = grid;
            globalInventoryUI.buttonPrefab = buttonPrefab;
            globalInventoryUI.inventory = inventory;
            globalInventoryUI.OnClick = (itemName) => OnEquip(itemName);
            globalInventoryUI.onPointerEnter = (itemName) => OnPointerEnter(itemName);
            globalInventoryUI.onPointerExit = (itemName) => OnPointerExit(itemName);
            globalInventoryUI.Display();*/

            skillInventoryUI.grid = grid;
            skillInventoryUI.buttonPrefab = buttonPrefab;
            skillInventoryUI.OnClick = (commandName) => OnSelectCommand(commandName);
            skillInventoryUI.inventory = currentPartyMember.getSkills;
            skillInventoryUI.onPointerEnter = (itemName) => { };
            skillInventoryUI.onPointerExit = (itemName) => { };
            skillInventoryUI.Display();
        }

        public void OnClickItems()
        {
            currentState = State.item;
            itemInventoryUI.grid = grid;
            itemInventoryUI.buttonPrefab = buttonPrefab;
            itemInventoryUI.OnClick = (commandName) => OnSelectCommand(commandName);
            itemInventoryUI.inventory = Global.instance.inventories[Factory.instance.getBasic.name];
            itemInventoryUI.onPointerEnter = (itemName) => { };
            itemInventoryUI.onPointerExit = (itemName) => { };
            itemInventoryUI.Display();
        }

        public void Update()
        {
            if (commandName == "None")
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.transform == null)
                return;

            target = hit.transform.GetComponent<IActor>();

            if (target == null)
                return;

            //need an animation that shows selected target here.

            if (Input.GetMouseButtonDown(0))
            {
                OnSelectTarget(target); 
            }
        }

        public void OnSelectCommand(string commandName)
        {
            this.commandName = commandName;
        }

        public void OnSelectTarget(IActor target)
        {
            Command command = new Command(currentPartyMember, Factory.instance.GetItem(commandName), new List<IActor> { target });
            Global.instance.pendingCommands.AddLast(command);
            currentPartyMember.getATBGuage.Reset();
            Global.instance.aTBGuageFilledQueue.Dequeue();
            gameObject.SetActive(false);
        }
    }
}