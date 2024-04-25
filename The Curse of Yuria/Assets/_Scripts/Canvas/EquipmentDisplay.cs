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

        void Start()
        {
            inventoryUI = GameObject.Find("/DontDestroyOnLoad").GetComponent<IInventoryUI>();
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            helmetsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Helmet, global.getHelmets); });
            earringsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Earrings, global.getEarrings); });
            glassesTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Glasses, global.getGlasses); });
            meleeWeapons1HTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.MeleeWeapon1H, global.getMeleeWeapons1H); });
            meleeWeapons2HTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.MeleeWeapon2H, global.getMeleeWeapons2H); });
            capesTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Cape, global.getCapes); });
            armorTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Armor, global.getArmor); });
            shieldsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Shield, global.getShields); });
            bowsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(EquipmentPart.Bow, global.getShields); });

            partyMemberIndex = 0;
            OnClickEquipmentPartTab(EquipmentPart.Helmet, global.getHelmets);
        }
        
        public void OnClickEquipmentPartTab(EquipmentPart part, IInventory inventory)
        {
            currentPart = part;
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
            helmetSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Helmet)].getSprite;
            earringSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Earrings)].getSprite;
            glassesSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Glasses)].getSprite;
            meleeWeapon1HSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.MeleeWeapon1H)].getSprite;
            meleeWeapon2HSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.MeleeWeapon1H)].getSprite;
            capeSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Cape)].getSprite;
            armorSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Armor)].getSprite;
            shieldSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Shield)].getSprite;
            bowsSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(EquipmentPart.Bow)].getSprite;
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