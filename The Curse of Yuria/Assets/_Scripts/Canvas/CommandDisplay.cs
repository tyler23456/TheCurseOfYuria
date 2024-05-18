using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    public class CommandDisplay : MonoBehaviour
    {
        IGlobal global;
        IFactory factory;

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
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            skillInventoryUI = new InventoryUI(factory);
            itemInventoryUI = new InventoryUI(factory);

            currentPartyMember = global.aTBGuageFilledQueue.Peek();

            attackTab.onClick.RemoveAllListeners();
            magicTab.onClick.RemoveAllListeners();
            itemTab.onClick.RemoveAllListeners();

            attackTab.onClick.AddListener(() => OnClickAttack());
            magicTab.onClick.AddListener(() => OnClickSkill());
            itemTab.onClick.AddListener(() => OnClickItems());

            commandName = "None";

            OnClickAttack();

            IGlobal.gameState = IGlobal.GameState.Paused;
        }

        private void OnDisable()
        {
            IGlobal.gameState = IGlobal.GameState.Playing;
        }

        public void OnClickAttack()
        {
            currentState = State.attack;

            string weapon = currentPartyMember.getEquipment.Find(i => 
            factory.GetItem(i).category == IItem.Category.meleeWeapons1H ||
            factory.GetItem(i).category == IItem.Category.meleeWeapons2H ||
            factory.GetItem(i).category == IItem.Category.bows);

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
            itemInventoryUI.inventory = global.inventories[IItem.Category.supplies];
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
            Command command = new Command(currentPartyMember, factory.GetItem(commandName), new IActor[] { target });
            global.pendingCommands.Enqueue(command);
            currentPartyMember.getATBGuage.Reset();
            global.aTBGuageFilledQueue.Dequeue();
            gameObject.SetActive(false);
        }
    }
}