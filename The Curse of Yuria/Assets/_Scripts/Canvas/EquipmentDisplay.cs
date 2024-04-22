using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TCOY.Canvas
{
    public class EquipmentDisplay : MonoBehaviour
    {
        [SerializeField] Button helmetsTab;
        [SerializeField] Button earringsTab;
        [SerializeField] Button glassesTab;
        [SerializeField] Button meleeWeapon1HTab;
        [SerializeField] Button meleeWeapon2HTab;
        [SerializeField] Button capesTab;
        [SerializeField] Button armorTab;
        [SerializeField] Button shieldTab;
        [SerializeField] Button bowsTab;

        [SerializeField] Button riverTab;
        [SerializeField] Button sarahTab;
        [SerializeField] Button nathanTab;
        [SerializeField] Button ashlynTab;
        [SerializeField] Button juelTab;
        [SerializeField] Button ninaTab;
        [SerializeField] Button onyxTab;

        [SerializeField] Image partyMemberSprite;
        [SerializeField] Text itemInfo;
        [SerializeField] Text partyMemberInfo;

        [SerializeField] Text partyMemberName;
        [SerializeField] Image helmetSlot;
        [SerializeField] Image earringSlot;
        [SerializeField] Image glassesSlot;
        [SerializeField] Image weaponSlot;
        [SerializeField] Image capeSlot;
        [SerializeField] Image armorSlot;
        [SerializeField] Image shieldSlot;

        [SerializeField] RectTransform root;

        IInventoryUI inventoryUI;
        IGlobal global;
        IFactory factory;
        IEquipment.Part currentPart = IEquipment.Part.helmet;
        int partyMemberIndex = 0;

        void Start()
        {
            inventoryUI = GameObject.Find("/DontDestroyOnLoad").GetComponent<IInventoryUI>();
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            helmetsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IEquipment.Part.helmet, global.getHelmets); });
            earringsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IEquipment.Part.earring, global.getEarrings); });
            glassesTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IEquipment.Part.glasses, global.getGlasses); });
            meleeWeapon1HTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IEquipment.Part.weapon, global.getMeleeWeapons1H); });
            meleeWeapon2HTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IEquipment.Part.weapon, global.getMeleeWeapons2H); });
            capesTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IEquipment.Part.cape, global.getCapes); });
            armorTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IEquipment.Part.armor, global.getArmor); });
            shieldTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IEquipment.Part.weapon, global.getShields); });

            int partyCount = global.getParty.Count;

            if (partyCount >= 1)
                riverTab.onClick.AddListener(() => OnClickPartyMemberTab(0));

            if (partyCount >= 2)
                sarahTab.onClick.AddListener(() => OnClickPartyMemberTab(1));

            if (partyCount >= 3)
                nathanTab.onClick.AddListener(() => OnClickPartyMemberTab(2));

            if (partyCount >= 4)
                ashlynTab.onClick.AddListener(() => OnClickPartyMemberTab(3));

            if (partyCount >= 5)
                juelTab.onClick.AddListener(() => OnClickPartyMemberTab(4));

            if (partyCount >= 6)
                ninaTab.onClick.AddListener(() => OnClickPartyMemberTab(5));

            if (partyCount >= 7)
                onyxTab.onClick.AddListener(() => OnClickPartyMemberTab(6));
        }
        
        public void OnClickEquipmentPartTab(IEquipment.Part part, IInventory inventory)
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
            OnClickPartyMemberTab(partyMemberIndex);
        }

        public void OnClickPartyMemberTab(int partyMemberIndex)
        {
            this.partyMemberIndex = partyMemberIndex;
            helmetSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(IEquipment.Part.helmet)].getSprite;
            earringSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(IEquipment.Part.earring)].getSprite; ;
            glassesSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(IEquipment.Part.glasses)].getSprite;
            weaponSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(IEquipment.Part.weapon)].getSprite;
            capeSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(IEquipment.Part.cape)].getSprite; ;
            armorSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(IEquipment.Part.armor)].getSprite; ;
            shieldSlot.sprite = factory.itemPrefabs[global.getParty[partyMemberIndex].getEquipment.GetPart(IEquipment.Part.shield)].getSprite; ;
        }

        public void Show()
        {
            root.gameObject.SetActive(true);
            OnClickEquipmentPartTab(IEquipment.Part.helmet, global.getHelmets);
            partyMemberIndex = 0;
        }

        public void Hide()
        {
            root.gameObject.SetActive(false);
        }
    }
}