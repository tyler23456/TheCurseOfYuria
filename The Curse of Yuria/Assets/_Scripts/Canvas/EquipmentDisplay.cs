using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;

namespace TCOY.Canvas
{
    public class EquipmentDisplay : MonoBehaviour
    {
        IInventoryUI inventoryUI;
        IGlobal global;
        IFactory factory;

        [SerializeField] RectTransform root;

        [SerializeField] Button helmetsTab;
        [SerializeField] Button earringsTab;
        [SerializeField] Button glassesTab;
        [SerializeField] Button meleeWeapons1HTab;
        [SerializeField] Button meleeWeapons2HTab;
        [SerializeField] Button capesTab;
        [SerializeField] Button armorTab;
        [SerializeField] Button shieldsTab;
        [SerializeField] Button bowsTab;
    
        [SerializeField] Text itemName;
        [SerializeField] Text itemInfo;

        [SerializeField] Image partyMemberSprite;
        [SerializeField] Text partyMemberName;
        [SerializeField] Text partyMemberStats;
        [SerializeField] Text partyMemberValues;

        [SerializeField] Image helmetSlot;
        [SerializeField] Image earringSlot;
        [SerializeField] Image glassesSlot;
        [SerializeField] Image meleeWeapon1HSlot;
        [SerializeField] Image meleeWeapon2HSlot;
        [SerializeField] Image capeSlot;
        [SerializeField] Image armorSlot;
        [SerializeField] Image shieldSlot;
        [SerializeField] Image bowsSlot;

        EquipmentPart currentPart = EquipmentPart.Helmet;
        int partyMemberIndex = 0;

        void OnEnable()
        {
            inventoryUI = GameObject.Find("/DontDestroyOnLoad").GetComponent<IInventoryUI>();
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            helmetsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Helmet, global.inventories[IItem.Category.helmets]); });
            earringsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Earrings, global.inventories[IItem.Category.earrings]); });
            glassesTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Glasses, global.inventories[IItem.Category.glasses]); });
            meleeWeapons1HTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.MeleeWeapon1H, global.inventories[IItem.Category.meleeWeapons1H]); });
            meleeWeapons2HTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.MeleeWeapon2H, global.inventories[IItem.Category.meleeWeapons2H]); });
            capesTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Cape, global.inventories[IItem.Category.capes]); });
            armorTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Armor, global.inventories[IItem.Category.armor]); });
            shieldsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Shield, global.inventories[IItem.Category.shields]); });
            bowsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Bow, global.inventories[IItem.Category.bows]); });

            partyMemberIndex = 0;
            OnClickEquipmentPartTab(EquipmentPart.Helmet, global.inventories[(int)IItem.Category.helmets]);
        }
        
        public void OnClickEquipmentPartTab(EquipmentPart part, IInventory inventory)
        {
            currentPart = part;
            inventoryUI.origin = new Vector2Int(182, 110);
            inventoryUI.isVertical = false;
            inventoryUI.OnClick = (itemName) => OnEquip(itemName);;
            inventoryUI.inventory = inventory;
            inventoryUI.buttonParent = root;
            inventoryUI.Show();
        }

        public void OnEquip(string itemName)
        {
            global.getParty[partyMemberIndex].getEquipment.Equip(currentPart, itemName);
            RefreshPartyMember();
        }

        public void RefreshPartyMember()
        {
            helmetSlot.sprite = factory.GetItem(global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Helmet)).icon;
            earringSlot.sprite = factory.GetItem(global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Earrings)).icon;
            glassesSlot.sprite = factory.GetItem(global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Glasses)).icon;
            meleeWeapon1HSlot.sprite = factory.GetItem(global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.MeleeWeapon1H)).icon;
            meleeWeapon2HSlot.sprite = factory.GetItem(global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.MeleeWeapon1H)).icon;
            capeSlot.sprite = factory.GetItem(global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Cape)).icon;
            armorSlot.sprite = factory.GetItem(global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Armor)).icon;
            shieldSlot.sprite = factory.GetItem(global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Shield)).icon;
            bowsSlot.sprite = factory.GetItem(global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Bow)).icon;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                partyMemberIndex--;
                partyMemberIndex = Mathf.Clamp(partyMemberIndex, 0, global.getParty.Count - 1);
                RefreshPartyMember();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                partyMemberIndex++;
                partyMemberIndex = Mathf.Clamp(partyMemberIndex, 0, global.getParty.Count - 1);
                RefreshPartyMember();
            }
        }
    }
}