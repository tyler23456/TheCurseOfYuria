using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;

namespace TCOY.Canvas
{
    public class CommandDisplay : MonoBehaviour
    {
        [SerializeField] RectTransform display;

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

        GameObject selectedIcon;

        void OnEnable()
        {
            selectedIcon.gameObject.SetActive(false);
            display.gameObject.SetActive(true);

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

            //OnClickAttack();

            Global.instance.gameState = Global.GameState.Paused;
        }

        private void OnDisable()
        {
            Global.instance.gameState = Global.GameState.Playing;
            MouseHover.instance.SetState(MouseHover.State.None);
        }

        public void OnClickAttack()
        {
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
            skillInventoryUI.displayName = true;
            skillInventoryUI.displayCount = false;
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

            if (selectedIcon == null)
                selectedIcon = Instantiate(Factory.instance.gameObject);

            selectedIcon.transform.position = target.getGameObject.transform.position + Vector3.up * target.getCollider2D.bounds.size.y;

            if (Input.GetMouseButtonDown(0))
            {
                OnSelectTarget(target);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                commandName = "None";
                display.gameObject.SetActive(true);
                MouseHover.instance.SetState(MouseHover.State.None);
            }
        }

        public void OnSelectCommand(string commandName)
        {
            this.commandName = commandName;
            MouseHover.instance.SetState(MouseHover.State.SelectTarget);
            selectedIcon.SetActive(true);
            display.gameObject.SetActive(false);
        }

        public void OnSelectTarget(IActor target)
        {
            Command command = new Command(currentPartyMember, Factory.instance.GetItem(commandName), new List<IActor> { target });
            Global.instance.pendingCommands.AddLast(command);
            currentPartyMember.getATBGuage.Reset();
            Global.instance.aTBGuageFilledQueue.Dequeue();
            MouseHover.instance.SetState(MouseHover.State.None);
            selectedIcon.gameObject.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }
}