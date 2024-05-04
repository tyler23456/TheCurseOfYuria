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

        void OnEnable()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            skillInventoryUI = new InventoryUI(factory);
            itemInventoryUI = new InventoryUI(factory);

            currentPartyMember = global.aTBGuageFilledQueue.Dequeue();

            attackTab.onClick.AddListener(() => OnClickAttack());
            magicTab.onClick.AddListener(() => OnClickSkill());
            attackTab.onClick.AddListener(() => OnClickItems());
        }

        public void OnClickAttack()
        {
            string weapon = currentPartyMember.getEquipment.GetPart(IItem.Category.meleeWeapons2H); //need to get current weapon
            OnSelectCommand(weapon);
        }

        public void OnClickSkill()
        {
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
            global.commandQueue.Enqueue(command);
            currentPartyMember.getATBGuage.Reset();
            gameObject.SetActive(false);
        }
    }
}