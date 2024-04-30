using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    public class CommandDisplay : MonoBehaviour
    {
        IInventoryUI inventoryUI;
        IGlobal global;
        IFactory factory;

        RectTransform root;

        [SerializeField] Button attackTab;
        [SerializeField] Button magicTab;
        [SerializeField] Button itemTab;

        int currentPartyMember = 0;
        string commandName = "None";
        IActor target = null;

        void Start()
        {
            inventoryUI = GameObject.Find("/DontDestroyOnLoad").GetComponent<IInventoryUI>();
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            currentPartyMember = (int)global.aTBGuageFilledQueue.Dequeue();

            attackTab.onClick.AddListener(() => OnClickAttack());
            magicTab.onClick.AddListener(() => OnClickMagic());
            attackTab.onClick.AddListener(() => OnClickItems());
        }

        public void OnClickAttack()
        {
            OnSelectCommand("Attack");
        }

        public void OnClickMagic()
        {
            inventoryUI.isVertical = true;
            inventoryUI.OnClick = (commandName) => OnSelectCommand(commandName);
            inventoryUI.inventory = global.getParty[currentPartyMember].getMagic;
            inventoryUI.buttonParent = root;
            inventoryUI.Show();
        }

        public void OnClickItems()
        {
            inventoryUI.isVertical = true;
            inventoryUI.OnClick = (commandName) => OnSelectCommand(commandName);
            inventoryUI.inventory = global.inventories[IItem.Category.supplies];
            inventoryUI.buttonParent = root;
            inventoryUI.Show();
        }

        public void Update()
        {
            if (commandName == "None")
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

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
            //global.commandQueue.Enqueue((global.getParty[currentPartyMember], (ISkill)factory.GetItem(commandName), targets));
        }
    }
}